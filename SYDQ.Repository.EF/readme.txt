1.Enable-Migrations (get-help Add-Migration –detailed)
2.Add-Migration Modify_Account_DateTime_To_Nullable
3.Update-Database –TargetMigration: InitialCreate 
4.Update-Database 
5.Update-Database -Script -Verbose
6.to empty database: Update-Database –TargetMigration: $InitialDatabase
7.Update-Database -SourceMigration:InitialCreate -TargetMigration:Modify_Account_DateTime_To_Nullable