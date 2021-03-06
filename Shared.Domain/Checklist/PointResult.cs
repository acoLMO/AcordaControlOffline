﻿using System;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public class PointResult : LeafResult
    {
        public PredefinedDefect PredefinedDefect { get; private set; }

        public PointResult(string conjunctElementCode, string elementCode, string shortName, string name = "") 
            : base(conjunctElementCode, elementCode, shortName, name)
        { }

        protected override Result AddChild(string sortKey, ITreeNode<Result> treeNode)
        {
            base.AddChild(sortKey, treeNode);
            return this;
        }

        public PointResult SetDefect(PredefinedDefect predefinedDefect)
        {
            PredefinedDefect = predefinedDefect ?? throw new ArgumentNullException();
            return this;
        }
    }
}