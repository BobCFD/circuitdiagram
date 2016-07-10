﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CircuitDiagram.Circuit.Metadata;
using CircuitDiagram.Primitives;

namespace CircuitDiagram.Circuit
{
    public class CircuitDocument
    {
        public CircuitDocument()
        {
            Elements = new List<IElement>();
            Metadata = new CircuitDocumentMetadata();
        }

        public CircuitDocumentMetadata Metadata { get; }

        public Size Size { get; set; }

        public ICollection<IElement> Elements { get; }

        public IEnumerable<IPositionalElement> PositionalElements => Elements.Where(el => el is IPositionalElement).Cast<IPositionalElement>();

        public IEnumerable<Component> Components => Elements.Where(el => el is Component).Cast<Component>();

        public IEnumerable<PositionalComponent> PositionalComponents => Elements.Where(el => el is PositionalComponent).Cast<PositionalComponent>(); 

        public IEnumerable<Wire> Wires => Elements.Where(el => el is Wire).Cast<Wire>();

        public IEnumerable<IConnectedElement> ConnectedElements => Elements.Where(el => el is IConnectedElement).Cast<IConnectedElement>();
    }
}
