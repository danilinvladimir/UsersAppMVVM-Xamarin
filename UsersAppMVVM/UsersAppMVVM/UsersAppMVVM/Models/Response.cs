using System;

namespace UsersAppMVVM.Models
{
    public class Response
    {
        public Results[] results { get; set; }
    }

    public class Results
    {
        public Name name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public Picture picture { get; set; }
    }

    public class Name
    {
        public string title { get; set; }
        public string first { get; set; }
        public string last { get; set; }
    }

    public class Picture
    {
        public Uri large { get; set; }
        public Uri medium { get; set; }
        public Uri thumbnail { get; set; }
    }
}
