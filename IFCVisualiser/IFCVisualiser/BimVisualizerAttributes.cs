using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Attributes;

namespace IFCVisualiser
{
    internal class BimVisualizerAttributes : GH_ComponentAttributes

    {
        private readonly BimVisualiser _component;
        public int H;
        public int W;
        private readonly List<RectangleF> _boundingBoxes;


        public BimVisualizerAttributes(IGH_Component component)
            : base(component)
        {
            _component = (BimVisualiser) component;
            _boundingBoxes = new List<RectangleF>();
        }


        protected override void Layout()
        {
            H = 500;
            W = 400;
            base.Layout();
            Bounds = GH_Convert.ToRectangle(new RectangleF(this.Pivot, (SizeF) new Size(W, H)));
            var boundsOut = (RectangleF) GH_Convert.ToRectangle(new RectangleF(this.Pivot, (SizeF) new Size(W - 80, H)));
            var boundsC =
                (RectangleF)
                    GH_Convert.ToRectangle(new RectangleF(new PointF(this.Pivot.X + W/2, this.Pivot.Y + H/2),
                        (SizeF) new Size(0, 0)));
            var boundsIn =
                (RectangleF)
                    GH_Convert.ToRectangle(new RectangleF(new PointF(this.Pivot.X + 60, this.Pivot.Y),
                        (SizeF) new Size(W, H)));

            m_innerBounds = boundsC;
            LayoutInputParams(_component, boundsIn);
            LayoutOutputParams(_component, boundsOut);
        }

        protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
        {
            base.Render(canvas, graphics, channel);

            var demBounds =
                (RectangleF)
                    GH_Convert.ToRectangle(new RectangleF(new PointF(this.Pivot.X + 60, this.Pivot.Y),
                        (SizeF) new Size(W - 60 - 80, H)));

            GH_Palette palette = GH_Palette.White;
            GH_Capsule capsule = GH_Capsule.CreateCapsule(demBounds, palette);
            capsule.Render(graphics, Selected, Owner.Locked, true);

            capsule.Dispose();
            capsule = null;

            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;
            format.LineAlignment = StringAlignment.Center;
            format.Trimming = StringTrimming.EllipsisCharacter;

            // Content rectangle
            RectangleF textRectangle = demBounds;
            textRectangle.Height = 20;
            for (int i = 0; i < BimVisualiserData.Instance.NameList.Count; i++)
            {
                _boundingBoxes.Insert(i, textRectangle);

                // Max entries value (it would be nice extending the Bounds if more)
                if (i == 25)
                    break;
                if (i == BimVisualiserData.Instance.Selected)
                    graphics.DrawString(BimVisualiserData.Instance.NameList[i], GH_FontServer.Standard,
                        Brushes.DodgerBlue, textRectangle, format);
                else
                    graphics.DrawString(BimVisualiserData.Instance.NameList[i], GH_FontServer.Standard, Brushes.Black,
                        textRectangle, format);
                format.Alignment = StringAlignment.Near;
                //textRectangle.Inflate(-5, 0);
                textRectangle.Y += 20;
            }
            // Always dispose of any GDI+ object that implement IDisposable.
            format.Dispose();
        }

        public override GH_ObjectResponse RespondToMouseDoubleClick(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            for (int i = 0; i < _boundingBoxes.Count; i++)
            {
                if (_boundingBoxes[i].Contains(e.CanvasLocation))
                {
                    BimVisualiserData.Instance.Selected = i;
                    _component.setReturnValue(BimVisualiserData.Instance.Selected);
                    _component.ExpirePreview(true);
                    _component.ExpireSolution(true);
                    return GH_ObjectResponse.Handled;
                    break;
                }
            }
            BimVisualiserData.Instance.Selected = -1;
            return GH_ObjectResponse.Handled;
        }
    }
}