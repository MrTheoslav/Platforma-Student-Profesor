﻿using API.Authorization;
using API.Interfaces;
using DAL;
using Microsoft.AspNetCore.Authorization;
using MODEL.DTO;
using MODEL.Models;

namespace API.Services
{
    public class AssigmentService : IAssigmentService
    {
        private readonly DataContext _context;
        private readonly IAuthorizationService _authorizationService;

        private readonly IUserContextService _userContextService;


        public AssigmentService(DataContext context, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _context = context;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }


        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public bool AssignmentExists(string name)
        {
            return _context.Assignments.Any(a => a.Name == name);
        }

        public bool AssignmentExists(int id)
        {
            return _context.Assignments.Any(a => a.AssignmentID == id);
        }

        public bool AddAssignment(Assignment assignment)
        {
            //var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, assignment, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            //if (!authorizationResult.Succeeded)
            //{
            //    return false;
            //}

            _context.Add(assignment);

            if (!Save())
                return false;

            foreach (UserRepository user in _context.UsersRepository)
            {
                if (user.RepositoryID == assignment.RepositoryID)
                {
                    var userAssignment = new UserAssigmnent
                    {
                        UserID = user.UserID,
                        AssigmnentID = assignment.AssignmentID
                    };

                    if (!AddStudentToAssignment(userAssignment))
                        return false;
                }
            }

            return true;
        }

        public bool AddStudentToAssignment(UserAssigmnent userAssignment)
        {
            _context.Add(userAssignment);

            return Save();
        }

        public bool UpdateAssignment(Assignment assignment)
        {
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, assignment, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                return false;
            }

            _context.Update(assignment);

            return Save();
        }

        public bool DeleteAssignment(Assignment assignment)
        {
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, assignment, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                return false;
            }

            _context.Remove(assignment);

            return Save();
        }

        public string GetRepositoryName(int assignmentID)
        {
            return _context.Repository.Where(r => r.RepositoryID == _context.Assignments.Where(a => a.AssignmentID == assignmentID).FirstOrDefault().RepositoryID).FirstOrDefault().Name;
        }

        public ICollection<Assignment> GetAssignmentsForRepository(int repositoryID)
        {
            return _context.Assignments.Where(x => x.RepositoryID == repositoryID).ToList();

        }

        public Assignment GetAssignmentByID(int id)
        {
            return _context.Assignments.Where(a => a.AssignmentID == id).First();
        }

        public bool UserAssigmnentExists(int assignmentID, int userID)
        {
            return _context.UserAssigmnents.Where(ua => ua.AssigmnentID == assignmentID && ua.UserID == userID).Any();
        }
        public bool UserAssigmnentExists(int assignmentID)
        {
            return _context.UserAssigmnents.Where(ua => ua.AssigmnentID == assignmentID).Any();
        }

        public UserAssigmnent GetUserAssigmnent(int assignmentID, int userID)
        {
            return _context.UserAssigmnents.Where(ua => ua.AssigmnentID == assignmentID && ua.UserID == userID).First();
        }

        public ICollection<UserAssigmnent> GetUserAssigmnents(int assignmentID)
        {
            return _context.UserAssigmnents.Where(ua => ua.AssigmnentID == assignmentID).ToList();
        }

        public bool CommentOrMark(UserAssigmnent userAssigmnent)
        {
            _context.UserAssigmnents.Update(userAssigmnent);
            return Save();
        }
    }
}
