﻿namespace Zeus.JsonAsserts
{
    public class ExpectiationResult
    {
        private ExpectiationResult(Expectation expectation, string[] errors)
        {
            Expectation = expectation;
            Errors = errors;
        }

        public static ExpectiationResult Success(Expectation expectation)
        {
            return new ExpectiationResult(expectation, null);
        }

        public static ExpectiationResult Error(Expectation expectation, params string[] Errors)
        {
            return new ExpectiationResult(expectation, Errors);
        }

        public Expectation Expectation { get; set; }

        public bool IsSuccess { get { return Failed == false; } }

        public bool Failed { get { return Errors != null && Errors.Length > 0; } }

        public string[] Errors { get; set; }
    }
}