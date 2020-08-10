using System;

namespace Git_system.Models.ECom.API.Process
{
    public static class Membership
    {
        private const int membershipR = 1;
        private const int membershipRInter = 2;
        private const int membershipIndividual = 3;
        private const int memberhsipIndividualInter = 4;
        private const int membershipNonIndividual = 5;
        private const int membershipNonIndividualInter = 6;

        /// <summary>
        /// Check language for membership
        /// </summary>
        /// <param name="membershipType">Type number of membership</param>
        /// <returns>1 and 2. 1 thai, 2 eng</returns>
        public static int checkMembershipLanguage(int membershipType)
        {
            if (Array.Exists(new int[] { membershipIndividual, membershipNonIndividual }, e => e == membershipType))
                membershipType = membershipR;
            else if (Array.Exists(new int[] { memberhsipIndividualInter, membershipNonIndividualInter }, e => e == membershipType))
                membershipType = membershipRInter;
            return membershipType;
        }

        /// <summary>
        /// Check currency of membership.
        /// </summary>
        /// <param name="membershipType">Type number of membership.</param>
        /// <returns>currency wiht lower.</returns>
        public static string checkCurrencyOfMembership(int membershipType)
        {
            string currency = "THB";
            currency = checkMembershipLanguage(membershipType) == membershipR ? "THB" : "USD";
            return currency;
        }
    }
}