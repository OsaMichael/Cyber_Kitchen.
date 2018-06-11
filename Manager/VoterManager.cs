using Cyber_Kitchen.Interface;
using Cyber_Kitchen.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Manager
{
    public class VoterManager : IVoterManager
    {
        private ApplicationDbContext _context;

        public VoterManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public Operation<VoterModel> CreateVoter(VoterModel model)
        {
            return Operation.Create(() =>
            {
                //model.Validate();
                var isExists = _context.Voters.Where(c => c.VotName == model.VotName).FirstOrDefault();
                if (isExists != null) throw new Exception("restaurant already exist");

                var entity = model.Create(model);
                _context.Voters.Add(entity);
                _context.SaveChanges();

                return model;
            });
        }

        public Operation<VoterModel[]> GetVoters()
        {
            return Operation.Create(() =>
            {
                var entities = _context.Voters.ToList();

                var models = entities.Select(c => new VoterModel(c)).ToArray();
                return models;
            });
        }
        public Operation<VoterModel> UpdateVoter(VoterModel model)
        {
            return Operation.Create(() =>
            {
                //model.Validate();
                var isExist = _context.Voters.Find(model.VoterId);
                if (isExist == null) throw new Exception("Voter does not exist");

                var entity = model.Edit(isExist, model);
                _context.Entry(entity);
                _context.SaveChanges();


                return model;
            });
        }
        public Operation<VoterModel> GetVoterById(int voterId)
        {
            return Operation.Create(() =>
            {
                var entity = _context.Voters.Where(c => c.VoterId == voterId).FirstOrDefault();
                if (entity == null) throw new Exception("Voter does not exist");
                return new VoterModel(entity);

            });
        }

        public Operation Details(int id)
        {
            return Operation.Create(() =>
            {
                var entity = _context.Voters.Include(s => s.VoterId == id).FirstOrDefault();
                if (entity == null) throw new Exception("Voter does not  exist");
                return new VoterModel(entity);


            });
        }
        public Operation DeleteVoter(int id)
        {
            return Operation.Create(() =>
            {
                var entity = _context.Voters.Find(id);
                if (entity == null) throw new Exception("Voter does not exist");

                _context.Voters.Remove(entity);
                _context.SaveChanges();
            });
        }
    }
}