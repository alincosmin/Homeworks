﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PrivateServiceClient.ServiceManager {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceManager.IServiceManager")]
    public interface IServiceManager {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceManager/InitialConnect", ReplyAction="http://tempuri.org/IServiceManager/InitialConnectResponse")]
        string InitialConnect(System.Guid id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceManager/InitialConnect", ReplyAction="http://tempuri.org/IServiceManager/InitialConnectResponse")]
        System.Threading.Tasks.Task<string> InitialConnectAsync(System.Guid id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceManager/RequestToAccessService", ReplyAction="http://tempuri.org/IServiceManager/RequestToAccessServiceResponse")]
        string[] RequestToAccessService(string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceManager/RequestToAccessService", ReplyAction="http://tempuri.org/IServiceManager/RequestToAccessServiceResponse")]
        System.Threading.Tasks.Task<string[]> RequestToAccessServiceAsync(string message);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceManagerChannel : PrivateServiceClient.ServiceManager.IServiceManager, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceManagerClient : System.ServiceModel.ClientBase<PrivateServiceClient.ServiceManager.IServiceManager>, PrivateServiceClient.ServiceManager.IServiceManager {
        
        public ServiceManagerClient() {
        }
        
        public ServiceManagerClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceManagerClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceManagerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceManagerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string InitialConnect(System.Guid id) {
            return base.Channel.InitialConnect(id);
        }
        
        public System.Threading.Tasks.Task<string> InitialConnectAsync(System.Guid id) {
            return base.Channel.InitialConnectAsync(id);
        }
        
        public string[] RequestToAccessService(string message) {
            return base.Channel.RequestToAccessService(message);
        }
        
        public System.Threading.Tasks.Task<string[]> RequestToAccessServiceAsync(string message) {
            return base.Channel.RequestToAccessServiceAsync(message);
        }
    }
}
