This example uses NServiceBus, and asymmetric database storage (EF/SQL Server for read model, 
db4o for event store - the latter is ideal for case of many different event types)

Commands: CQRSDemo.Web -> Unicast message -> CQRSDemo.DomainAC
command handlers use domain model aggregate to perform business logic and 
create events, events are stored in db4o database eventstore.db4o (in bin/debug folder)

Events: CQRSDemo.DomainAC -> Unicast message -> CQRSDemo.ReadModelAC
event handlers store updated read model using EF/SQL Server, context is called ReadContext, 
no connection string configured so currently expects database CQRSDemo.ReadModelAC.ReadContext on local server,
should be created by code-first if it doesn't exist
(events are only published to read model, so using unicast, not pub-sub)