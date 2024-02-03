using Grasshopper;
using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace AutoValueList
{
    public class AutoValueListInfo : GH_AssemblyInfo
    {
        public override string Name => "AutoValueList";

        //Return a 24x24 pixel bitmap to represent this GHA library.
        public override Bitmap Icon => null;

        //Return a short string describing the purpose of this GHA library.
        public override string Description => "";

        public override Guid Id => new Guid("1027a0d4-68ab-4253-ba2b-157a575598a1");

        //Return a string identifying you or your company.
        public override string AuthorName => "";

        //Return a string representing your preferred contact details.
        public override string AuthorContact => "";
    }
}