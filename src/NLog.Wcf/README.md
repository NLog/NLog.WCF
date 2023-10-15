# NLog WCF Target

NLog WCF Target sends log messages to a NLog Receiver Service (using WCF or Web Services).

See the [NLog Wiki](https://github.com/NLog/NLog/wiki/LogReceiverService-target) for available options and examples.

## Register Extension

NLog will only recognize type-alias `LogReceiverService` when loading from `NLog.config`-file, if having added extension to `NLog.config`-file:

```xml
<extensions>
    <add assembly="NLog.WCF"/>
</extensions>
```

Alternative register from code using [fluent configuration API](https://github.com/NLog/NLog/wiki/Fluent-Configuration-API):

```csharp
LogManager.Setup().SetupExtensions(ext => ext.RegisterTarget<NLog.Targets.LogReceiverWebServiceTarget>());
```
