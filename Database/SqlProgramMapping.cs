#region � Copyright Web Applications (UK) Ltd, 2015.  All rights reserved.
// Copyright (c) 2015, Web Applications UK Ltd
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of Web Applications UK Ltd nor the
//       names of its contributors may be used to endorse or promote products
//       derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL WEB APPLICATIONS UK LTD BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
#endregion

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using WebApplications.Utilities.Annotations;
using WebApplications.Utilities.Database.Schema;
using WebApplications.Utilities.Logging;

namespace WebApplications.Utilities.Database
{
    /// <summary>
    /// Maps a <see cref="SqlProgram"/> to a <see cref="SqlProgramDefinition"/>.
    /// </summary>
    [PublicAPI]
    public class SqlProgramMapping : DbMapping
    {
        /// <summary>
        /// Gets the mapping for the <paramref name="program"/> given from the specified <paramref name="schema"/>.
        /// </summary>
        /// <param name="program">The program to get the mapping for.</param>
        /// <param name="connection">The connection the mapping is for.</param>
        /// <param name="schema">The schema to get the mapping from.</param>
        /// <param name="checkOrder">If set to <see langword="true" /> check the order of the parameters.</param>
        /// <returns>The mapping.</returns>
        internal static SqlProgramMapping GetMapping(
            [NotNull] SqlProgram program,
            [NotNull] Connection connection,
            [NotNull] DatabaseSchema schema,
            bool checkOrder)
        {
            // Find the program
            if (!schema.ProgramsByName.TryGetValue(program.Text, out SqlProgramDefinition programDefinition))
                throw new LoggingException(
                    LoggingLevel.Critical,
                    () => Resources.SqlProgram_Validate_DefinitionsNotFound,
                    program.Text,
                    program.Name);
            Debug.Assert(programDefinition != null);

            // Validate parameters
            IReadOnlyList<SqlProgramParameter> parameters =
                programDefinition.ValidateParameters(program.Parameters, checkOrder);

            return new SqlProgramMapping(connection, programDefinition, parameters);
        }

        /// <summary>
        ///   The underlying <see cref="SqlProgramDefinition">program definition</see>.
        /// </summary>
        [NotNull]
        public readonly SqlProgramDefinition Definition;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlProgramMapping" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="definition">The definition.</param>
        /// <param name="parameters">The parameters.</param>
        private SqlProgramMapping(
            [NotNull] Connection connection,
            [NotNull] SqlProgramDefinition definition,
            [NotNull] IReadOnlyList<SqlProgramParameter> parameters)
            : base(connection, parameters, definition.ParameterNameComparer)
        {
            Definition = definition ?? throw new ArgumentNullException(nameof(definition));
        }

        /// <summary>
        /// Appends the text for executing the program to the <paramref name="builder"/> given.
        /// </summary>
        /// <param name="builder">The builder to append to.</param>
        /// <param name="parameters">The parameters passed to the program.</param>
        public override void AppendExecute(SqlStringBuilder builder, ParametersCollection parameters)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            SqlProgramDefinition def = Definition;

            builder.Append("EXECUTE ");

            // If there is a return value parameter, need to assign the result to it
            if (parameters.ReturnValueParameter != null)
            {
                builder
                    .Append(parameters.ReturnValueParameter.BaseParameter.ParameterName)
                    .Append(" = ");
            }

            builder
                .AppendIdentifier(def.SqlSchema.FullName)
                .Append('.')
                .AppendIdentifier(def.Name);

            bool first = true;
            foreach (DbBatchParameter parameter in parameters.Parameters)
            {
                // Already dealt with return value parameter
                if (parameter.Direction == ParameterDirection.ReturnValue)
                    continue;

                if (first) first = false;
                else builder.Append(',');

                builder
                    .AppendLine()
                    .Append('\t')
                    .Append(parameter.ProgramParameter.FullName)
                    .Append(" = ")
                    .Append(parameter.BaseParameter.ParameterName);

                // If the parameter value is Out<T>, need to add OUT to actually get the return value
                if (parameter.IsOutputUsed)
                    builder.Append(" OUT");
            }

            builder.AppendLine(";");
        }
    }
}