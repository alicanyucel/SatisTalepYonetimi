#!/bin/bash
set -e

echo "Waiting for SQL Server to start..."
until /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -Q 'SELECT 1' &> /dev/null
do
  echo "SQL Server is starting up..."
  sleep 5
done

echo "SQL Server is ready!"

exec dotnet SatisTalepYonetimi.WebAPI.dll