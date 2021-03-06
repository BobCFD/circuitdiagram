﻿// Circuit Diagram http://www.circuit-diagram.org/
// 
// Copyright (C) 2016  Samuel Fisher
// 
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using CircuitDiagram.Circuit;
using CircuitDiagram.Document.InternalWriter;
using CircuitDiagram.IO;
using CircuitDiagram.IO.Document.InternalWriter;

namespace CircuitDiagram.Document
{
    /// <summary>
    /// Writes documents in the Circuit Diagram Document format.
    /// <see cref="http://www.circuit-diagram.org/help/cddx-file-format"/>
    /// </summary>
    public sealed class CircuitDiagramDocumentWriter : ICircuitWriter
    {
        private readonly PackageManager packageManager;
        private readonly MainDocumentWriter mainDocumentWriter;

        public CircuitDiagramDocumentWriter()
        {
            packageManager = new PackageManager();
            mainDocumentWriter = new MainDocumentWriter();
        }

        public string FileTypeName => "Circuit Diagram Document";

        public string FileTypeExtension => ".cddx";

        public void WriteCircuit(CircuitDocument document, Stream stream)
        {
            // Cannot write directly to a file stream, so write to this and copy later
            using (var ms = new MemoryStream())
            {
                // Create the package that contains the document files
                using (var package = Package.Open(ms, FileMode.Create))
                {
                    // Write the main document. This contains the connection and layout data.
                    using (var mainDocStream = packageManager.CreateMainDocumentPart(package))
                        mainDocumentWriter.Write(document, mainDocStream);

                    // Write the metadata. This contains document size, author, created time etc.

                    // Write any additional resources
                }

                ms.WriteTo(stream);
            }
        }
    }
}
