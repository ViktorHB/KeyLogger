﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UI.ServiceReferenceUI {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReferenceUI.IUIService")]
    public interface IUIService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUIService/LiveSearch", ReplyAction="http://tempuri.org/IUIService/LiveSearchResponse")]
        string[] LiveSearch(string key);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUIService/LiveSearch", ReplyAction="http://tempuri.org/IUIService/LiveSearchResponse")]
        System.Threading.Tasks.Task<string[]> LiveSearchAsync(string key);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUIService/GetAllUsers", ReplyAction="http://tempuri.org/IUIService/GetAllUsersResponse")]
        string[] GetAllUsers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUIService/GetAllUsers", ReplyAction="http://tempuri.org/IUIService/GetAllUsersResponse")]
        System.Threading.Tasks.Task<string[]> GetAllUsersAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUIService/GetAllMessages", ReplyAction="http://tempuri.org/IUIService/GetAllMessagesResponse")]
        string[] GetAllMessages(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUIService/GetAllMessages", ReplyAction="http://tempuri.org/IUIService/GetAllMessagesResponse")]
        System.Threading.Tasks.Task<string[]> GetAllMessagesAsync(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUIService/GetMessegesOnDate", ReplyAction="http://tempuri.org/IUIService/GetMessegesOnDateResponse")]
        string[] GetMessegesOnDate(string userName, System.DateTime date);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUIService/GetMessegesOnDate", ReplyAction="http://tempuri.org/IUIService/GetMessegesOnDateResponse")]
        System.Threading.Tasks.Task<string[]> GetMessegesOnDateAsync(string userName, System.DateTime date);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUIServiceChannel : UI.ServiceReferenceUI.IUIService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UIServiceClient : System.ServiceModel.ClientBase<UI.ServiceReferenceUI.IUIService>, UI.ServiceReferenceUI.IUIService {
        
        public UIServiceClient() {
        }
        
        public UIServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UIServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UIServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UIServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string[] LiveSearch(string key) {
            return base.Channel.LiveSearch(key);
        }
        
        public System.Threading.Tasks.Task<string[]> LiveSearchAsync(string key) {
            return base.Channel.LiveSearchAsync(key);
        }
        
        public string[] GetAllUsers() {
            return base.Channel.GetAllUsers();
        }
        
        public System.Threading.Tasks.Task<string[]> GetAllUsersAsync() {
            return base.Channel.GetAllUsersAsync();
        }
        
        public string[] GetAllMessages(string userName) {
            return base.Channel.GetAllMessages(userName);
        }
        
        public System.Threading.Tasks.Task<string[]> GetAllMessagesAsync(string userName) {
            return base.Channel.GetAllMessagesAsync(userName);
        }
        
        public string[] GetMessegesOnDate(string userName, System.DateTime date) {
            return base.Channel.GetMessegesOnDate(userName, date);
        }
        
        public System.Threading.Tasks.Task<string[]> GetMessegesOnDateAsync(string userName, System.DateTime date) {
            return base.Channel.GetMessegesOnDateAsync(userName, date);
        }
    }
}
