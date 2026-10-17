using System.Diagnostics;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static InheritanceApp.IndividualMember;

namespace InheritanceApp
{

    public enum MembershipTier{Basic, Standard, Premium};

    public interface IBillable
    {
        decimal MonthlyCharge();
        string AccountReference { get; }
    }

    public abstract class Member : IBillable
    {
        public string Name { get; set; }
        public MembershipTier Tier { get; set; }
        public DateRange Membership { get; set; }
        public string AccountReference { get; }


        public Member(string _name, MembershipTier _tier)
        {
            if (string.IsNullOrEmpty(_name))
            {
                throw new ArgumentException();
            }

            Name = _name;
            Tier = _tier;
            AccountReference = "MEM-" + Name.ToUpper();
        }

        public abstract decimal MonthlyCharge();

        public void PrintStatement()
        {
            Console.WriteLine($"The user's name is : {Name}, his membership is {Membership} and the charge is {MonthlyCharge()}");
        }
    }

    public class IndividualMember : Member
    {

        public string Name { get; set; }
        public MembershipTier Tier { get; set; }
        public DateRange Membership { get; set; }

        public IndividualMember(string _name, MembershipTier _tier, DateRange _membership) : base(_name, _tier)
        {
            Name = _name;
            Tier = _tier;
            Membership = _membership;
        }
        public override decimal MonthlyCharge()
        {
            switch (Tier)
            {
                case MembershipTier.Basic:
                    return (decimal)20;
                    break;
                case MembershipTier.Standard:
                    return 35;
                    break;
                case MembershipTier.Premium:
                    return 50;
                    break;

                default:
                    return 0;
                    break;
            }
        }

        public static MembershipTier ParseTier(string input)
        {
            bool result;
            MembershipTier status;
            //I had to look this up
            result = Enum.TryParse(input, out status);

            if (result == true)
            {
                return status;
            }

            else
            {
                Console.WriteLine("Error with Parsing the Tier");
                return MembershipTier.Basic;
            }
        }
    }

    public class FamilyMember : Member
    {

        public int nameame { get; set; }
        public MembershipTier Tier { get; set; }
        public DateRange Membership { get; set; }
        public int MemberCount { get; set; }

        public FamilyMember(string _name, MembershipTier _tier, DateRange _membership, int _membercount) : base(_name, _tier)
        {
            Name = _name;
            Tier = _tier;
            Membership = _membership;

            if(_membercount < 2)
            {
                throw new Exception();
            }
            MemberCount = _membercount;
        }
        public override decimal MonthlyCharge()
        {
            switch (Tier)
            {
                case MembershipTier.Basic:
                    return (decimal) (20 * 0.8f * MemberCount);
                    break;
                case MembershipTier.Standard:
                    return  (decimal)(35* 0.8f * MemberCount);
                    break;
                case MembershipTier.Premium:
                    return  (decimal)(50 * 0.8f * MemberCount);
                    break;

                default:
                    return 0;
                    break;
            }
        }
    }

    public struct DateRange
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public double AmountOfDays { get; set; }
        public DateRange(DateTime _start, DateTime _end)
        {
            if (End < Start)
            {
                throw new ArgumentOutOfRangeException();
            }

            Start = _start;
            End = _end;
        }
        public double Days()
        {
            return AmountOfDays = (End - Start).TotalDays; // I needed to research this line
        }

        public bool Contains(DateTime date)
        {
            if (date < End && date > Start)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    class Program
    {

        static void Main()
        {
            List<Member> membersLit = new List<Member>();

            Member newMember1 = new Member("John", MembershipTier.Premium, );
        }
    }
}
