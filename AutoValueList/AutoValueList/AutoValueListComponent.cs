using Grasshopper.Kernel;
using Grasshopper.Kernel.Special;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoValueList
{
    public class AutoValueListComponent : GH_Component
    {
        public AutoValueListComponent()
          : base("AutoValueList", "AVL",
            "",
            "Params", "Input")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Key", "K", "", GH_ParamAccess.list);
            pManager.AddGenericParameter("Value", "V", "", GH_ParamAccess.list);
            pManager.AddGenericParameter("ValueList", "L", "", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            object vl_input = default;
            DA.GetData("ValueList",ref vl_input);

            GH_ValueList valueList = (GH_ValueList)Params.Input.Find(p => p.Name == "ValueList").Sources.FirstOrDefault(p =>
            {
                if (!p.GetType().IsAssignableFrom(typeof(GH_ValueList)))
                    return false;
                return p.VolatileData.AllData(true).Any(ig => ig == vl_input);
            });

            if (valueList == null)
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "Incorrect input of ValueList.");
                return;
            }
            valueList.ListItems.Clear();

            List<string> keys = new List<string>();
            List<string> values = new List<string>();
            DA.GetDataList("Key", keys);
            DA.GetDataList("Value", values);

            var kvs = keys.Zip(values, (key, value) => new { key = key, value = value });

            foreach(var item in kvs)
                valueList.ListItems.Add(new GH_ValueListItem(item.key, $"\"{item.value}\""));

            valueList.ExpireSolution(true);
        }
        public override GH_Exposure Exposure => GH_Exposure.secondary;
        protected override System.Drawing.Bitmap Icon => null;
        public override Guid ComponentGuid => new Guid("de75b8a1-776f-429e-b681-d9f8bdf33612");
    }
}