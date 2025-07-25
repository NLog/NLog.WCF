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
    using System.Collections.Generic;
    using System.Diagnostics;

    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    /// <summary>
    /// Wire format for NLog event package.
    /// </summary>
    [DataContract(Name = "events", Namespace = LogReceiverServiceConfig.WebServiceNamespace)]
    [XmlType(Namespace = LogReceiverServiceConfig.WebServiceNamespace)]
    [XmlRoot("events", Namespace = LogReceiverServiceConfig.WebServiceNamespace)]
    [DebuggerDisplay("Count = {Events.Length}")]
    public class NLogEvents
    {
        /// <summary>
        /// Gets or sets the name of the client.
        /// </summary>
        /// <value>The name of the client.</value>
        [DataMember(Name = "cli", Order = 0)]
        [XmlElement("cli", Order = 0)]
        public string? ClientName { get; set; }

        /// <summary>
        /// Gets or sets the base time (UTC ticks) for all events in the package.
        /// </summary>
        /// <value>The base time UTC.</value>
        [DataMember(Name = "bts", Order = 1)]
        [XmlElement("bts", Order = 1)]
        public long BaseTimeUtc { get; set; }

        /// <summary>
        /// Gets or sets the collection of layout names which are shared among all events.
        /// </summary>
        /// <value>The layout names.</value>
        [DataMember(Name = "lts", Order = 100)]
        [XmlArray("lts", Order = 100)]
        [XmlArrayItem("l")]
        public StringCollection? LayoutNames { get; set; }

        /// <summary>
        /// Gets or sets the collection of logger names.
        /// </summary>
        /// <value>The logger names.</value>
        [DataMember(Name = "str", Order = 200)]
        [XmlArray("str", Order = 200)]
        [XmlArrayItem("l")]
        public StringCollection? Strings { get; set; }

        /// <summary>
        /// Gets or sets the list of events.
        /// </summary>
        /// <value>The events.</value>
        [DataMember(Name = "ev", Order = 1000)]
        [XmlArray("ev", Order = 1000)]
        [XmlArrayItem("e")]
        public NLogEvent[]? Events { get; set; }

        /// <summary>
        /// Converts the events to sequence of <see cref="LogEventInfo"/> objects suitable for routing through NLog.
        /// </summary>
        /// <param name="loggerNamePrefix">The logger name prefix to prepend in front of each logger name.</param>
        /// <returns>
        /// Sequence of <see cref="LogEventInfo"/> objects.
        /// </returns>
        public IList<LogEventInfo> ToEventInfo(string loggerNamePrefix)
        {
            return ToEventInfoArray(loggerNamePrefix);
        }

        /// <summary>
        /// Converts the events to sequence of <see cref="LogEventInfo"/> objects suitable for routing through NLog.
        /// </summary>
        /// <returns>
        /// Sequence of <see cref="LogEventInfo"/> objects.
        /// </returns>
        public IList<LogEventInfo> ToEventInfo()
        {
            return ToEventInfo(string.Empty);
        }

        internal LogEventInfo[] ToEventInfoArray(string loggerNamePrefix)
        {
            if (Events is null || Events.Length == 0)
            {
#if NET35
                return new LogEventInfo[0];
#else
                return System.Array.Empty<LogEventInfo>();
#endif
            }

            var result = new LogEventInfo[Events.Length];
            var hasPrefix = !string.IsNullOrEmpty(loggerNamePrefix);

            for (int i = 0; i < result.Length; ++i)
            {
                var loggerName = Strings?[Events[i].LoggerOrdinal] ?? string.Empty;
                result[i] = Events[i].ToEventInfo(this, hasPrefix ? (loggerNamePrefix + loggerName) : loggerName);
            }

            return result;
        }
    }
}