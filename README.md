# NLog.WCF
NLog [WCF target](https://github.com/NLog/NLog/wiki/LogReceiverService-target) for sending LogEvents to NLog Receiver Service (using WCF or Web Services)

[![Version](https://badge.fury.io/nu/NLog.WCF.svg)](https://www.nuget.org/packages/NLog.WCF)
[![AppVeyor](https://img.shields.io/appveyor/ci/nlog/NLog-WCF/master.svg)](https://ci.appveyor.com/project/nlog/NLog-WCF/branch/master)


### How to install

1) Install the package

    `Install-Package NLog.WCF` or in your csproj:

    ```xml
    <PackageReference Include="NLog.WCF" Version="5.*" />
    ```

2) Add to your nlog.config:

    ```xml
    <extensions>
        <add assembly="NLog.WCF"/>
    </extensions>
    ```

### How to use LogReceiverWebServiceTarget

Use the target "LogReceiverService" in your nlog.config

```xml
<nlog>
    <extensions>
        <add assembly="NLog.WCF"/>
    </extensions>
    <targets>
      <target xsi:type="LogReceiverService"
              name="wcf"
              endpointAddress="String">
      </target>
    </targets>
    <rules>
        <logger minLevel="Info" writeTo="wcf" />
    </rules>
</nlog>
```