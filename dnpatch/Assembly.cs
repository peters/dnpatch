﻿using System;
using dnlib.DotNet;

namespace dnpatch
{
    public class Assembly
    {
        public AssemblyInfo AssemblyInfo;
        public AssemblyData AssemblyData;
        public AssemblyModel AssemblyModel;

        internal Assembly(AssemblyInfo assemblyInfo)
        {
            AssemblyInfo = assemblyInfo;
            AssemblyData = new AssemblyData() // Load assembly data
            {
                Module = ModuleDefMD.Load(AssemblyInfo.Name)
            };
            AssemblyModel = new AssemblyModel();
        }

        public void SetNamespace(string @namespace)
        {
            AssemblyModel.Namespace = @namespace; 
        }

        public void SetType(string classPath)
        {
            string path = $"{AssemblyModel.Namespace}.{classPath}";
            TypeDef type = AssemblyData.Module.FindReflection(path);
            AssemblyModel.Type = type ?? throw new Exception($"Type '{path}' does not exist.");
        }

        public void SetField(string fieldName)
        {
            AssemblyModel.Field = AssemblyModel.Type.FindField(fieldName) ?? throw new Exception($"Field '{AssemblyModel.Type.FullName}.{fieldName}' does not exist.");
        }

        public void SetMethod(string methodName) 
        {
            AssemblyModel.Method = AssemblyModel.Type.FindMethod(methodName) ?? throw new Exception($"Method '{AssemblyModel.Type.FullName}.{methodName}' does not exist.");
        }

		public void SetProperty(string propertyName)
		{
            AssemblyModel.Property = AssemblyModel.Type.FindProperty(propertyName) ?? throw new Exception($"Property '{AssemblyModel.Type.FullName}.{propertyName}' does not exist.");
		}

		public void SetEvent(string eventName)
		{
            AssemblyModel.Event = AssemblyModel.Type.FindEvent(eventName) ?? throw new Exception($"Event '{AssemblyModel.Type.FullName}.{eventName}' does not exist.");
		}
    }   
}
