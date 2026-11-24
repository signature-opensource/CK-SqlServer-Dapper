A thin wrapper enabling Dapper on the `ISqlConnectionController` of CK.SqlServer that transparently uses its current
transaction, and maps the `IPoco` interfaces to their generated types.

The `SqlConnectionControllerExtension` methods (in the `Dapper` namespace) are the Dapper ones: `Execute`, `Query`,
`QueryFirst`, `QuerySingle` and their variants.
