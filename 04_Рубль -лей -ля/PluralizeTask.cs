namespace Pluralize
{
    public static class PluralizeTask
    {
		    public static string PluralizeRubles(int count)
		    {
		        int countDiv = count % 100;
            if ((countDiv > 4) && (countDiv < 21))
                return "рублей"; 
            if ((count % 10 > 1) && (count % 10 < 5))
                return "рубля";
            if ((count % 10 > 4) && (count % 10 <= 9) || (count % 10 == 0))
                return "рублей";
            return "рубль";	
		    }
	  }
}	
