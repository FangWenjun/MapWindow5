﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MW5.Api.Interfaces;
using MW5.Plugins.Enums;
using MW5.Tools.Model;
using MW5.Tools.Model.Parameters;

namespace MW5.Tools.Tools.Geoprocessing.VectorGeometryTools
{
    [GisTool(GroupKeys.VectorGeometryTools)]
    public class BufferTool : GisToolBase
    {
        [Input("Layer to build buffer for", 0)]
        public VectorLayerParameter InputLayer { get; set; }

        [Input("Buffer distance", 1)]
        public DoubleParameter BufferDistance { get; set; }

        [OptionalInput("Number of segments", 2), DefaultValue(30)]
        public IntegerParameter NumSegments { get; set; }

        [Input("Merge results", 3)]
        public BooleanParameter MergeResults { get; set; }

        [Input("Save results as", 4), DefaultValue("Buffer")]
        public OutputLayerParameter Output { get; set; }

        /// <summary>
        /// Gets name of the tool.
        /// </summary>
        public override string Name
        {
            get { return "Buffer by distance"; }
        }

        /// <summary>
        /// Gets description of the tool.
        /// </summary>
        public override string Description
        {
            get { return "Builds a buffer around features of input vector layer."; }
        }

        /// <summary>
        /// Provide execution logic for the tool.
        /// </summary>
        public override bool Run()
        {
            var fs = InputLayer.Value.BufferByDistance(BufferDistance.Value, NumSegments.Value, false, MergeResults.Value);
            if (fs != null)
            {
                HandleOutput(fs, Output.Value);
                return true;
            }

            return false;
        }
    }
}
