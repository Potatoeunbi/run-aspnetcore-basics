﻿using AspnetRunBasics.Data;
using AspnetRunBasics.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AspnetRunBasics.Repositories
{
    public class ContactRepository : IContactRepository
    {
        protected readonly AspnetRunContext _dbContext;

        public ContactRepository(AspnetRunContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Contact> SendMessage(Contact contact)
        {
            _dbContext.Contacts.Add(contact);
            await _dbContext.SaveChangesAsync();
            return contact;
        }

        public async Task<Contact> GetLogInAsync(string Email, string Pwd)
        {
            return await _dbContext.Contacts
                .FirstOrDefaultAsync(x => x.Email == Email && x.Message == Pwd);
        }

        public async Task<Contact> Subscribe(string address)
        {
            // implement your business logic
            var newContact = new Contact();
            newContact.Email = address;
            newContact.Message = address;
            newContact.Name = address;

            _dbContext.Contacts.Add(newContact);
            await _dbContext.SaveChangesAsync();

            return newContact;
        }
    }
}
