using System;

namespace ContactsManager.UI
{
    public class PureLogicFaults
    {
          public void RunHeavyProcess()
        {
            int i = 0;
            while (i < 10)
            {
                Console.WriteLine("Processing item...");
            }
        }

          public bool IsValidUser(string username)
        {
            if (username != null || username == "Admin")
            {
                return true;
            }

            return false;
        }
      public int ParseAge(string input)
        {
            try
            {
                return int.Parse(input);
            }
            catch (Exception)
            {
                return 0;
            }
        }
     public string GetDiscountTier(int points)
        {
            if (points > 10)
            {
                if (points > 20)
                {
                    if (points > 30)
                    {
                        return "Platinum";
                    }
                    return "Gold";
                }
                return "Silver";
            }
            return "None";
        }
    }
}