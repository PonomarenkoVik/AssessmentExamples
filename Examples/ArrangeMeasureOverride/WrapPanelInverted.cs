using System;
using System.Windows;
using System.Windows.Controls;

namespace ArrangeMeasureOverride
{
    public class WrapPanelInverted : WrapPanel
    {
        public bool IsInverted
        {
            get { return (bool)GetValue(IsInvertedProperty); }
            set { SetValue(IsInvertedProperty, value); }
        }

        public static readonly DependencyProperty IsInvertedProperty =
            DependencyProperty.Register("IsInverted", typeof(bool), typeof(WrapPanelInverted), new PropertyMetadata(false));



        protected override Size ArrangeOverride(Size finalSize)
        {
            int firstInLine = 0;
            double itemWidth = ItemWidth;
            double itemHeight = ItemHeight;
            double accumulatedV = 0;
            double itemU = (Orientation == Orientation.Horizontal ? itemWidth : itemHeight);
            UVSize curLineSize = new UVSize(Orientation);
            UVSize uvFinalSize = new UVSize(Orientation, finalSize.Width, finalSize.Height);
            bool itemWidthSet = !DoubleUtil.IsNaN(itemWidth);
            bool itemHeightSet = !DoubleUtil.IsNaN(itemHeight);
            bool useItemU = (Orientation == Orientation.Horizontal ? itemWidthSet : itemHeightSet);

            UIElementCollection children = InternalChildren;

            for (int i = 0, count = children.Count; i < count; i++)
            {
                UIElement child = children[i] as UIElement;
                if (child == null) continue;
                UVSize sz = new UVSize(
                    Orientation,
                    (itemWidthSet ? itemWidth : child.DesiredSize.Width),
                    (itemHeightSet ? itemHeight : child.DesiredSize.Height));

                if (DoubleUtil.GreaterThan(curLineSize.U + sz.U, uvFinalSize.U)) //need to switch to another line
                {
                    ArrangeLine(accumulatedV, curLineSize.V, firstInLine, i, useItemU, itemU, finalSize);

                    accumulatedV += curLineSize.V;
                    curLineSize = sz;

                    if (DoubleUtil.GreaterThan(sz.U, uvFinalSize.U)) //the element is wider then the constraint - give it a separate line                    
                    {
                        //switch to next line which only contain one element
                        ArrangeLine(accumulatedV, sz.V, i, ++i, useItemU, itemU, finalSize);

                        accumulatedV += sz.V;
                        curLineSize = new UVSize(Orientation);
                    }
                    firstInLine = i;
                }
                else //continue to accumulate a line
                {
                    curLineSize.U += sz.U;
                    curLineSize.V = Math.Max(sz.V, curLineSize.V);
                }
            }

            //arrange the last line, if any
            if (firstInLine < children.Count)
            {
                ArrangeLine(accumulatedV, curLineSize.V, firstInLine, children.Count, useItemU, itemU, finalSize);
            }

            return finalSize;
        }

        private void ArrangeLine(double v, double lineV, int start, int end, bool useItemU, double itemU, Size finalSize)
        {
            double u = 0;
            bool isHorizontal = (Orientation == Orientation.Horizontal);

            UIElementCollection children = InternalChildren;
            for (int i = start; i < end; i++)
            {
                UIElement child = children[i] as UIElement;
                if (child != null)
                {
                    UVSize childSize = new UVSize(Orientation, child.DesiredSize.Width, child.DesiredSize.Height);
                    double layoutSlotU = (useItemU ? itemU : childSize.U);

                    double width = GetWidthOrHeight(layoutSlotU, lineV, isHorizontal, true);
                    double height = GetWidthOrHeight(layoutSlotU, lineV, isHorizontal, false);
                    double x = GetXPosition(u, v, width, finalSize.Width, isHorizontal);
                    double y = GetYPosition(u, v, height, finalSize.Height, isHorizontal);

                    child.Arrange(new Rect(
                        x,
                        y,
                        width,
                        height));

                    u += layoutSlotU;
                }
            }
        }

        private double GetWidthOrHeight(double layoutSlotU, double lineV, bool isHorizontal, bool isWidth)
        {
            if (isWidth)
                return isHorizontal ? layoutSlotU : lineV;

            return isHorizontal ? lineV : layoutSlotU;
        }

        private double GetXPosition(double u, double v, double itemWidth, double finalPanelWidth, bool isHorizontal)
        {
            var x = isHorizontal ? u : v;
            return IsInverted ? finalPanelWidth - x - itemWidth : x;
        }

        private double GetYPosition(double u, double v, double itemHeight, double finalPanelHeight, bool isHorizontal)
        {
            var y = isHorizontal ? v : u;
            return IsInverted ? finalPanelHeight - y - itemHeight : y;
        }


        protected override Size MeasureOverride(Size constraint)
        {
            UVSize curLineSize = new UVSize(Orientation);
            UVSize panelSize = new UVSize(Orientation);
            UVSize uvConstraint = new UVSize(Orientation, constraint.Width, constraint.Height);
            double itemWidth = ItemWidth;
            double itemHeight = ItemHeight;
            bool itemWidthSet = !DoubleUtil.IsNaN(itemWidth);
            bool itemHeightSet = !DoubleUtil.IsNaN(itemHeight);

            Size childConstraint = new Size(
                (itemWidthSet ? itemWidth : constraint.Width),
                (itemHeightSet ? itemHeight : constraint.Height));

            UIElementCollection children = InternalChildren;

            for (int i = 0, count = children.Count; i < count; i++)
            {
                UIElement child = children[i] as UIElement;
                if (child == null) continue;

                //Flow passes its own constrint to children
                child.Measure(childConstraint);

                //this is the size of the child in UV space
                UVSize sz = new UVSize(
                    Orientation,
                    (itemWidthSet ? itemWidth : child.DesiredSize.Width),
                    (itemHeightSet ? itemHeight : child.DesiredSize.Height));

                if (DoubleUtil.GreaterThan(curLineSize.U + sz.U, uvConstraint.U)) //need to switch to another line
                {
                    panelSize.U = Math.Max(curLineSize.U, panelSize.U);
                    panelSize.V += curLineSize.V;
                    curLineSize = sz;

                    if (DoubleUtil.GreaterThan(sz.U, uvConstraint.U)) //the element is wider then the constrint - give it a separate line                    
                    {
                        panelSize.U = Math.Max(sz.U, panelSize.U);
                        panelSize.V += sz.V;
                        curLineSize = new UVSize(Orientation);
                    }
                }
                else //continue to accumulate a line
                {
                    curLineSize.U += sz.U;
                    curLineSize.V = Math.Max(sz.V, curLineSize.V);
                }
            }

            //the last line size, if any should be added
            panelSize.U = Math.Max(curLineSize.U, panelSize.U);
            panelSize.V += curLineSize.V;

            //go from UV space to W/H space
            return new Size(panelSize.Width, panelSize.Height);
        }


        private struct UVSize
        {
            internal UVSize(Orientation orientation, double width, double height)
            {
                U = V = 0d;
                _orientation = orientation;
                Width = width;
                Height = height;
            }

            internal UVSize(Orientation orientation)
            {
                U = V = 0d;
                _orientation = orientation;
            }

            internal double U;
            internal double V;
            private Orientation _orientation;

            internal double Width
            {
                get { return (_orientation == Orientation.Horizontal ? U : V); }
                set { if (_orientation == Orientation.Horizontal) U = value; else V = value; }
            }
            internal double Height
            {
                get { return (_orientation == Orientation.Horizontal ? V : U); }
                set { if (_orientation == Orientation.Horizontal) V = value; else U = value; }
            }
        }
    }
}
