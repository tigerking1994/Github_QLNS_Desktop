﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace VTS.QLNS.CTC.App.Helper
{
    public class WritableOptions<T> : IWritableOptions<T> where T : class, new()
    {
        //private readonly IHostingEnvironment _environment;
        //private readonly IOptionsMonitor<T> _options;
        //private readonly IConfigurationRoot _configuration;
        //private readonly string _section;
        //private readonly string _file;

        //public WritableOptions(
        //    IHostingEnvironment environment,
        //    IOptionsMonitor<T> options,
        //    IConfigurationRoot configuration,
        //    string section,
        //    string file)
        //{
        //    _environment = environment;
        //    _options = options;
        //    _configuration = configuration;
        //    _section = section;
        //    _file = file;
        //}

        //public T Value => _options.CurrentValue;
        //public T Get(string name) => _options.Get(name);

        //public void Update(Action<T> applyChanges)
        //{
        //    var fileProvider = _environment.ContentRootFileProvider;
        //    var fileInfo = fileProvider.GetFileInfo(_file);
        //    var physicalPath = fileInfo.PhysicalPath;

        //    var jObject = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(physicalPath));
        //    var sectionObject = jObject.TryGetValue(_section, out JToken section) ?
        //        JsonConvert.DeserializeObject<T>(section.ToString()) : (Value ?? new T());

        //    applyChanges(sectionObject);

        //    jObject[_section] = JObject.Parse(JsonConvert.SerializeObject(sectionObject));
        //    File.WriteAllText(physicalPath, JsonConvert.SerializeObject(jObject, Formatting.Indented));
        //    _configuration.Reload();
        //}
        //public T Value => throw new NotImplementedException();

        //public void Update(Action<T> applyChanges)
        //{
        //    throw new NotImplementedException();
        //}
        public T Value => throw new NotImplementedException();

        public void Update(Action<T> applyChanges)
        {
            throw new NotImplementedException();
        }
    }
}
