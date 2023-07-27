﻿using JobsPlatform.Interfaces;

namespace JobsPlatform.Models
{
    public class Job:User
    {
        public string? Industry { get;set; }
        public  int? MinSalary { get;set; }
        public int? MaxSalary { get;set; }

        public Job(string?name , int?id,  string? industry, int? minSalary,int?maxSalary,int?identifier):base( name, identifier, id)
        {
            Industry = industry;
            MinSalary=minSalary;
            MaxSalary=maxSalary;
        }
    }
}
