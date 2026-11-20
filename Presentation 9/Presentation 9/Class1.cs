using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace A002_SelectElement
{
    [Transaction(TransactionMode.Manual)]
    public class A002_SelectElement : IExternalCommand
    {
        public Result Execute(
            ExternalCommandData commandData,
            ref string message,
            ElementSet elements)
        {
            // Lấy tài liệu Revit
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            //====================================================
            // 1. Đếm số đối tượng đang được chọn
            //====================================================

            ICollection<ElementId> selectElements = uidoc.Selection.GetElementIds();

            int total = selectElements.Count;

            MessageBox.Show("Bạn đã chọn " + total + " phần tử");

            //====================================================
            // 2. Hiển thị ID các đối tượng
            //====================================================

            StringBuilder idList = new StringBuilder();

            foreach (ElementId id in selectElements)
            {
                idList.AppendLine(id.IntegerValue.ToString());
            }

            MessageBox.Show(idList.ToString(), "Danh sách ID");

            //====================================================
            // 3. Chọn 3 điểm
            //====================================================

            XYZ p1 = uidoc.Selection.PickPoint("Chọn điểm thứ nhất");
            XYZ p2 = uidoc.Selection.PickPoint("Chọn điểm thứ hai");
            XYZ p3 = uidoc.Selection.PickPoint("Chọn điểm thứ ba");

            MessageBox.Show(
                "Điểm 1:\n" + p1 +
                "\n\nĐiểm 2:\n" + p2 +
                "\n\nĐiểm 3:\n" + p3,
                "Thông tin điểm");

            //====================================================
            // 4. Chọn nhiều cạnh (Edge)
            //====================================================

            IList<Reference> edges =
                uidoc.Selection.PickObjects(
                    ObjectType.Edge,
                    "Chọn các cạnh");

            MessageBox.Show(
                "Bạn đã chọn " + edges.Count + " cạnh.",
                "Edge");

            return Result.Succeeded;
        }
    }
}