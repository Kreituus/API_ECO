using NLog.Fluent;
using Serilog;
using Serilog.Core;
using Serilog.Enrichers;
using Serilog.Events;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using Log = Serilog.Log;

namespace Logs_Eco
{
    public class Logger : ILogger
    {
        private static string _pathLogFile;
        public const string header = "INFO: {0} DETALLE: {1}";
        private readonly string _user;
        public Logger()
        {
            string logDirectory = ConfigurationManager.AppSettings["LOG_DIRECTORY"];
            string level = ConfigurationManager.AppSettings["LOG_LEVEL"];
            Create(logDirectory, level);
        }

        public Logger(string pathLogFile, string level, string user)
        {
            Create(pathLogFile, level);
            this._user = user;
        }

        private static void Create(string pathLogFile, string level)
        {
            switch (level)
            {
                case "INFORMATION":
                    _pathLogFile = pathLogFile;
                    Log.Logger = new LoggerConfiguration()
                        .Enrich.With(new ThreadIdEnricher())
                        .WriteTo.File(_pathLogFile, rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] <{ThreadId}> {Message:lj}{NewLine}{Exception}")
                        .MinimumLevel.Information()
                        .CreateLogger();
                    break;
                case "FATAL":
                    _pathLogFile = pathLogFile;
                    Log.Logger = new LoggerConfiguration()
                        .Enrich.With(new ThreadIdEnricher())
                        .WriteTo.File(_pathLogFile, rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] <{ThreadId}> {Message:lj}{NewLine}{Exception}")
                        .MinimumLevel.Fatal()
                        .CreateLogger();
                    break;
                case "WARNING":
                    _pathLogFile = pathLogFile;
                    Log.Logger = new LoggerConfiguration()
                        .Enrich.With(new ThreadIdEnricher())
                        .WriteTo.File(_pathLogFile, rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] <{ThreadId}> {Message:lj}{NewLine}{Exception}")
                        .MinimumLevel.Warning()
                        .CreateLogger();
                    break;
                case "ERROR":
                    _pathLogFile = pathLogFile;
                    Log.Logger = new LoggerConfiguration()
                        .Enrich.With(new ThreadIdEnricher())
                        .WriteTo.File(_pathLogFile, rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] <{ThreadId}> {Message:lj}{NewLine}{Exception}")
                        .MinimumLevel.Error()
                        .CreateLogger();
                    break;
                case "DEBUG":
                    _pathLogFile = pathLogFile;
                    Log.Logger = new LoggerConfiguration()
                        .Enrich.With(new ThreadIdEnricher())
                        .WriteTo.File(_pathLogFile, rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] <{ThreadId}> {Message:lj}{NewLine}{Exception}")
                        .MinimumLevel.Debug()
                        .CreateLogger();
                    break;
                default:
                    _pathLogFile = pathLogFile;
                    Log.Logger = new LoggerConfiguration()
                        .Enrich.With(new ThreadIdEnricher())
                        .WriteTo.File(_pathLogFile, rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] <{ThreadId}> {Message:lj}{NewLine}{Exception}")
                        .MinimumLevel.Verbose()
                        .CreateLogger();
                    break;
            }
        }

        private static string GetStackTraceInfo()
        {
            var stackFrame = new StackTrace().GetFrame(2);
            string methodName = stackFrame.GetMethod().Name;
            string className = stackFrame.GetMethod().ReflectedType.FullName;
            return string.Format("Class: \"{0}\" Method: \"{1}\"", className, methodName);
        }

        public void Debug(string format, params object[] objects)
        {
            string location = GetStackTraceInfo();
            string message = string.Format(format, objects);
            string _header = header;
            if (!string.IsNullOrEmpty(_user))
                _header = $"USER: {_user} " + header;
            Log.Debug(string.Format(_header, location, message));
        }

        public void Debug(string message)
        {
            string location = GetStackTraceInfo();
            string _header = header;
            if (!string.IsNullOrEmpty(_user))
                _header = $"USER: {_user} " + header;
            Log.Debug(string.Format(_header, location, message));
        }

        public void Error(string format, params object[] objects)
        {
            string message = string.Format(format, objects);
            string location = GetStackTraceInfo();
            string _header = header;
            if (!string.IsNullOrEmpty(_user))
                _header = $"USER: {_user} " + header;
            Log.Error(string.Format(_header, location, message));
        }

        public void Error(string message)
        {
            string location = GetStackTraceInfo();
            string _header = header;
            if (!string.IsNullOrEmpty(_user))
                _header = $"USER: {_user} " + header;
            Log.Error(string.Format(_header, location, message));
        }

        public void Fatal(string format, params object[] objects)
        {
            string message = string.Format(format, objects);
            string location = GetStackTraceInfo();
            string _header = header;
            if (!string.IsNullOrEmpty(_user))
                _header = $"USER: {_user} " + header;
            Log.Fatal(string.Format(_header, location, message));
        }

        public void Fatal(string message)
        {
            string location = GetStackTraceInfo();
            string _header = header;
            if (!string.IsNullOrEmpty(_user))
                _header = $"USER: {_user} " + header;
            Log.Fatal(string.Format(_header, location, message));
        }

    }

}