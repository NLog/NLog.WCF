// 
// Copyright (c) 2004-2021 Jaroslaw Kowalski <jaak@jkowalski.net>, Kim Christensen, Julian Verdurmen
// 
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without 
// modification, are permitted provided that the following conditions 
// are met:
// 
// * Redistributions of source code must retain the above copyright notice, 
//   this list of conditions and the following disclaimer. 
// 
// * Redistributions in binary form must reproduce the above copyright notice,
//   this list of conditions and the following disclaimer in the documentation
//   and/or other materials provided with the distribution. 
// 
// * Neither the name of Jaroslaw Kowalski nor the names of its 
//   contributors may be used to endorse or promote products derived from this
//   software without specific prior written permission. 
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF 
// THE POSSIBILITY OF SUCH DAMAGE.
// 

namespace NLog.LogReceiverService
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Channels;

    /// <summary>
    /// Log Receiver Client using WCF.
    /// </summary>
    public sealed class WcfLogReceiverOneWayClient : WcfLogReceiverClientBase<ILogReceiverOneWayClient>, ILogReceiverOneWayClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WcfLogReceiverOneWayClient"/> class.
        /// </summary>
        public WcfLogReceiverOneWayClient()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WcfLogReceiverOneWayClient"/> class.
        /// </summary>
        /// <param name="endpointConfigurationName">Name of the endpoint configuration.</param>
        public WcfLogReceiverOneWayClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WcfLogReceiverOneWayClient"/> class.
        /// </summary>
        /// <param name="endpointConfigurationName">Name of the endpoint configuration.</param>
        /// <param name="remoteAddress">The remote address.</param>
        public WcfLogReceiverOneWayClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WcfLogReceiverOneWayClient"/> class.
        /// </summary>
        /// <param name="endpointConfigurationName">Name of the endpoint configuration.</param>
        /// <param name="remoteAddress">The remote address.</param>
        public WcfLogReceiverOneWayClient(string endpointConfigurationName, EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WcfLogReceiverOneWayClient"/> class.
        /// </summary>
        /// <param name="binding">The binding.</param>
        /// <param name="remoteAddress">The remote address.</param>
        public WcfLogReceiverOneWayClient(Binding binding, EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        /// <summary>
        /// Begins processing of log messages.
        /// </summary>
        /// <param name="events">The events to send.</param>
        /// <param name="callback">The callback.</param>
        /// <param name="asyncState">Asynchronous state.</param>
        /// <returns>
        /// IAsyncResult value which can be passed to <see cref="ILogReceiverOneWayClient.EndProcessLogMessages"/>.
        /// </returns>
        public override IAsyncResult BeginProcessLogMessages(NLogEvents events, AsyncCallback callback, object asyncState)
        {
            return Channel.BeginProcessLogMessages(events, callback, asyncState);
        }

        /// <summary>
        /// Ends asynchronous processing of log messages.
        /// </summary>
        /// <param name="result">The result.</param>
        public override void EndProcessLogMessages(IAsyncResult result)
        {
            Channel.EndProcessLogMessages(result);
        }
    }
}