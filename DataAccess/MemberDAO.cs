using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MemberDAO
    {
        FStoreDBAssignmentContext dBContext = new FStoreDBAssignmentContext();
        List<Member> members = new List<Member>();

        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object();

        private MemberDAO() { }

        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }

        public Member LoginByMember(string email, string password)
        {
            Member member = null;
            try
            {
                var context = new FStoreDBAssignmentContext();
                member = context.Members.SingleOrDefault(c => c.Email == email && c.Password == password);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return member;
        }

        // Add new member
        public void AddNew(Member member)
        {
            try
            {
                using FStoreDBAssignmentContext dbContext = new FStoreDBAssignmentContext();
                dbContext.Members.Add(member);
                dbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        // Add Update member
        public void Update(Member member)
        {
            /*try
            {
                using FStoreDBAssignmentContext dbContext = new FStoreDBAssignmentContext();
                dbContext.Entry<Member>(member).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }*/
            try
            {
                var mem = (from u in dBContext.Members where u.MemberId == member.MemberId select u).SingleOrDefault();
                if (mem != null)
                {
                    mem.MemberId = member.MemberId;
                    mem.Email = member.Email;
                    mem.CompanyName = member.CompanyName;
                    mem.City = member.City;
                    mem.Country = member.Country;
                    dBContext.SaveChanges();
                }
                else
                {
                    throw new Exception("Staff does not exitst");
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }


        public void Remove(int memberId)
        {
            Console.WriteLine($"member ID {memberId}");
            try
            {
                using FStoreDBAssignmentContext dBContext = new FStoreDBAssignmentContext();

                var member = dBContext.Members.SingleOrDefault(members => members.MemberId == memberId);
                //   Console.WriteLine("Lay ra được một member: "  + member);
                if (member != null)

                {
                    dBContext.Remove(member);
                    dBContext.SaveChanges();

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Member GetEmailAndPassword(string _Email, string _Password)
        {
            FStoreDBAssignmentContext dbContext = new FStoreDBAssignmentContext();
            Member? member = dbContext.Members.Where(mem => mem.Email == _Email && mem.Password == _Password).FirstOrDefault();
            return member;
        }

        public IEnumerable<Member> GetMemberList()
        {
            using FStoreDBAssignmentContext dbContext = new FStoreDBAssignmentContext();
            var members = dbContext.Members.ToList();
            return members;
        }

        public Member GetMemberById(int memberId)
        {
            Member mem = dBContext.Members.SingleOrDefault(p => p.MemberId == memberId);
            return mem;
        }
    }
}
