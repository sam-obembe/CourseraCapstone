#!/bin/sh

migrationName=$1
dotnet ef migrations add $migrationName