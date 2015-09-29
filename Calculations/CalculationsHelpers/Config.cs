using System;

namespace CalculationsHelpers
{
    public static class CalcConfig
    {
        // MailIn
        public static TimeSpan CheckMailStartDelay = TimeSpan.FromSeconds(0);
        public static TimeSpan CheckMailInterval = TimeSpan.FromMilliseconds(2000);
        public static int MaxNumberEmailsReceived = 10;

        public static int NetworkExceptionChance = 40000;
        public static int FatalExceptionChance = 100000;
        

        // Calculation
        public static int MaxResult = 400;
        public static int MessageProcessingTimeMinMs = 1000;
        public static int MessageProcessingTimeMaxMs = 10000;

        // Calculation coordinator
        public static int NumberOfCalculatorWorkers = 10;

        // MailOut
        public static int MailOutDelayMs = 500;
    }
}
