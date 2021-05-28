using System;

namespace Trails
{

    public class AddConstraintMenuAttribute : Attribute
    {
        public readonly string menuPath;
        public AddConstraintMenuAttribute(string mPath){
            
            menuPath = mPath;
        }
   
    }

    public class AddServiceMenuAttribute : Attribute
    {
        public readonly string menuPath;
        public AddServiceMenuAttribute(string mPath){
             
            menuPath = mPath; 
        }
    
    }

    public class AddNodeMenuAttribute : Attribute
    {
        public readonly string menuPath;
        public AddNodeMenuAttribute(string mPath){
            
            menuPath=mPath;
        }
   
    }

}