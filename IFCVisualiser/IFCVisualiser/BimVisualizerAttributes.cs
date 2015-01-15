using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;

namespace IFCVisualiser
{
    class BimVisualizerAttributes : Grasshopper.Kernel.Attributes.GH_ComponentAttributes

    {

        private BimVisualiser component;

        public BimVisualizerAttributes(IGH_Component component)
            : base(component)
        {
            component = (BimVisualiser) component;
        }
       


        protected override void Layout()
        {
            base.Layout();
        }

        public override void ExpireLayout()
        {    
            base.ExpireLayout();
        }

        protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
        {
            base.Render(canvas, graphics, channel);
            // Create a new Capsule without text or icon.
            // Define the default palette.
            GH_Palette palette = GH_Palette.Normal;
            GH_Capsule capsule = GH_Capsule.CreateCapsule(Bounds, palette);
            // Render the capsule using the current Selection, Locked and Hidden states.
            // Integer parameters are always hidden since they cannot be drawn in the viewport.
            capsule.Render(graphics, Selected, Owner.Locked, true);

            // Always dispose of a GH_Capsule when you're done with it.
            capsule.Dispose();
            capsule = null;

             StringFormat format = new StringFormat();
             format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            format.Trimming = StringTrimming.EllipsisCharacter;

            // Our entire capsule is 60 pixels high, and we'll draw 
            // three lines of text, each 20 pixels high.
            RectangleF textRectangle = Bounds;
            textRectangle.Height = 20;
            graphics.DrawString(Owner.NickName, GH_FontServer.Standard, Brushes.Black, textRectangle, format);
            format.Alignment = StringAlignment.Near;
            textRectangle.Inflate(-5, 0);

            textRectangle.Y += 20;
            graphics.DrawString("Yooo", GH_FontServer.StandardItalic, Brushes.Black, textRectangle, format);

            textRectangle.Y += 20;
            graphics.DrawString("OOo", GH_FontServer.StandardItalic, Brushes.Black, textRectangle, format);

            textRectangle.Y += 20;
            graphics.DrawString("OOo", GH_FontServer.StandardItalic, Brushes.Black, textRectangle, format);

            textRectangle.Y += 20;
            graphics.DrawString("OOo", GH_FontServer.StandardItalic, Brushes.Black, textRectangle, format);

            textRectangle.Y += 20;
            graphics.DrawString("OOo", GH_FontServer.StandardItalic, Brushes.Black, textRectangle, format);

            textRectangle.Y += 20;
            graphics.DrawString("OOo", GH_FontServer.StandardItalic, Brushes.Black, textRectangle, format);textRectangle.Y += 20;
            graphics.DrawString("OOo" ,GH_FontServer.StandardItalic, Brushes.Black,textRectangle, format);

            // Always dispose of any GDI+ object that implement IDisposable.
            format.Dispose();






        }



    }
}
