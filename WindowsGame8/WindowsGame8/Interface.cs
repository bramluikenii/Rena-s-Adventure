using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TDRPG
{
    
    
    class InterfaceElement
    {
        InterfaceElement[] elements;
        
        public Vector2 boxPositionRelative;
        public Vector2 boxPositionAbsolute;
        public Vector2 innerPositionAbsolute;
        public Vector2 boxSize;
        public Vector2 minSize;
	    public Vector2 innerSize;
        public bool splitHorizontal;
        int align;

	    void add(InterfaceElement IntElem){}
	    InterfaceElement(bool split, int alignTo, Vector2 size)
        {
            splitHorizontal = split;
            align = alignTo;
            minSize = size;
        }
	    void calculatePositions(Vector2 localOrigin){
            boxPositionAbsolute = localOrigin + boxPositionRelative;
            switch(align)
            {
                case 1: case 4: case 7:
                    innerPositionAbsolute.X = boxPositionAbsolute.X;
                    break;
                case 2: case 5: case 8:
                    innerPositionAbsolute.X = boxPositionAbsolute.X + (boxSize.X - innerSize.X) / 2;
                    break;
                case 3: case 6: case 9:
                    innerPositionAbsolute.X = boxPositionAbsolute.X + (boxSize.X - innerSize.X);
                    break;
            }
            switch (align)
            {
                case 1: case 2: case 3:
                    innerPositionAbsolute.Y = boxPositionAbsolute.Y;
                    break;
                case 4: case 5: case 6:
                    innerPositionAbsolute.Y = boxPositionAbsolute.Y + (boxSize.Y - innerSize.Y) / 2;
                    break;
                case 7: case 8: case 9:
                    innerPositionAbsolute.Y = boxPositionAbsolute.Y + (boxSize.Y - innerSize.Y);
                    break;
            }
            for (int i = 0; i < elements.GetLength(0); i++)
            {
                elements[i].calculatePositions(innerPositionAbsolute);
            }
	    }
	    void calculateSizes(){
		    Vector2 myInnerSize;
            myInnerSize.X = 0; myInnerSize.Y = 0;
            for (int i = 0; i < elements.GetLength(0); i++)
            {
                elements[i].calculateSizes();
                if (splitHorizontal)
                {
                    elements[i].boxPositionRelative.X = myInnerSize.X;
                    elements[i].boxPositionRelative.Y = 0;
                    myInnerSize.X += elements[i].innerSize.X;
                    if (elements[i].innerSize.Y > myInnerSize.Y) myInnerSize.Y = elements[i].innerSize.Y;
                }
                else
                {
                    elements[i].boxPositionRelative.Y = myInnerSize.Y;
                    elements[i].boxPositionRelative.X = 0;
                    myInnerSize.Y += elements[i].innerSize.Y;
                    if (elements[i].innerSize.X > myInnerSize.X) myInnerSize.X = elements[i].innerSize.X;
                }
            }
            if(myInnerSize.X > minSize.X){
			    innerSize.X = myInnerSize.X;
		    } else {
			    innerSize.X = minSize.X;
		    }
            if(myInnerSize.Y > minSize.Y){
			    innerSize.Y = myInnerSize.Y;
		    } else {
			    innerSize.Y = minSize.Y;
		    }
            for (int i = 0; i < elements.GetLength(0); i++)
            {
                if (splitHorizontal)
                {
                    elements[i].boxSize.Y = innerSize.Y;
                }
                else
                {
                    elements[i].boxSize.X = innerSize.X;
                }
            }
	    }
	    void draw(){}
    
    }
    class Interface
    {

    }
}
