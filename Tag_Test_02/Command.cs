#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Input;

#endregion

namespace Tag_Test_02
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document curDoc = uidoc.Document;

            // put any code needed for the form here

            // open form
            MyForm curForm = new MyForm()
            {
                Width = 800,
                Height = 450,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
                Topmost = true,
            };

            curForm.ShowDialog();

            // create some lists to hold the views
            List<View> flrPlans = Utils.GetAllViewsByNameContains(curDoc, "Annotation");
            List<View> dimPlans = Utils.GetAllViewsByNameContains(curDoc, "Dimension");

            // set variables for family names
            string tagDoor = "";
            string tagWndw = "";
            string markDoor = "";
            string markWndw = "";
            string opngDoor = "";
            string opngWndw = "";

            // get form data and do something

            // create a transaction
            using (Transaction trans = new Transaction(curDoc))
            {
                // start the transaction
                trans.Start("Tag all doors and windows");

                if (curForm.GetCheckBoxFlrs() == true)
                {
                    // loop through the annotation views
                    foreach (View flrPlan in flrPlans)
                    {
                        // tag all doors in view
                        Utils.TagAllUntaggedDoorsWindowsInView(curDoc, flrPlan, tagDoor);

                        // tag all windows in view
                        Utils.TagAllUntaggedDoorsWindowsInView(curDoc, flrPlan, tagWndw);
                    }
                }

                if (curForm.GetCheckBoxDouble() == true)
                {
                    // loop through the annotation views
                    foreach (View flrPlan in flrPlans)
                    {
                        // mark all doors in view
                        Utils.MarkAllDoorsWindowsInView(curDoc, flrPlan, markDoor);

                        // mark all windows in view
                        Utils.MarkAllDoorsWindowsInView(curDoc, flrPlan, markWndw);
                    }
                }

                if (curForm.GetCheckBoxDims() == true)
                {
                    // loop through the dimension views
                    foreach (View dimPlan in dimPlans)
                    {
                        // tag all door rough opening sizes
                        Utils.TagAllUntaggedDoorsWindowsInView(curDoc, dimPlan, opngDoor);

                        // tag all window rough opening sizes
                        Utils.TagAllUntaggedDoorsWindowsInView(curDoc, dimPlan, opngWndw);
                    }
                }

                // commit the transaction
                trans.Commit();
            }

            return Result.Succeeded;
        }

        public static String GetMethod()
        {
            var method = MethodBase.GetCurrentMethod().DeclaringType?.FullName;
            return method;
        }
    }
}
