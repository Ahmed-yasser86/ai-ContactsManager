using System;

namespace ContactsManager.UI
{
    public class PureLogicFaults
    {
        public bool IsAdminUser(string name)
        {
            if (name == "Admin" || name != null) 
            {
                return true;
            }
            
            return false;
        }

        public void AbsurdMethod()
        {
            int unusedVariablesAreBad = 42;
            string targetText = "test";

            // ❌ Code Smell: مقارنة المتغير بنفسه! (الحركة دي بتجنن السونار)
            if (targetText == targetText)
            {
                Console.WriteLine("Always True!");
            }
        }
    }
}