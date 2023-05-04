
.PHONY: all install_deps \
	new_solution \
	new_classlib \
	new_console \
	add_ref \
	add_ms_configuration \
	add_serilog \
	build \
	run  \
	tests \
	init_secret \
	add_secret \
	db_to_classes \

SLN_NAME?=WebNote2
RUN_PRJ=WebNoteApi
RUN_TEST=WebNote2.Tests

PROJ=$(NAME)/$(NAME).csproj
PROJ_TO=$(NAME_TO)/$(NAME_TO).csproj


CMD_ADD_TO_SOLUTION=dotnet sln add $(PROJ)

install_deps::
	-dotnet tool install --global dotnet-ef
	-dotnet tool update --global dotnet-ef

add_ms_configuration::
	-dotnet add $(NAME) package Microsoft.Extensions.Configuration
	-dotnet add $(NAME) package Microsoft.Extensions.Configuration.UserSecrets
	-dotnet add $(NAME) package Microsoft.Extensions.Configuration.EnvironmentVariables
	-dotnet add $(NAME) package Microsoft.Extensions.Configuration.Json
	-dotnet add $(NAME) package Microsoft.Extensions.DependencyInjection.Abstractions
	-dotnet add $(NAME) package Microsoft.Extensions.Configuration.Binder
	-dotnet add $(NAME) package Microsoft.Extensions.Configuration.CommandLine

add_serilog::
	-dotnet add $(NAME) package Serilog
	-dotnet add $(NAME) package Serilog.Enrichers.Environment
	-dotnet add $(NAME) package Serilog.Enrichers.Thread
	-dotnet add $(NAME) package Serilog.Settings.Configuration
	-dotnet add $(NAME) package Serilog.Sinks.Console
	-dotnet add $(NAME) package Serilog.Sinks.File
	 # -dotnet add $(NAME) package Serilog.Sinks.Fluentd.Revived

new_solution::
	dotnet new sln -n $(SLN_NAME)

new_classlib::
	dotnet new classlib -o $(NAME)
	dotnet sln add $(PROJ)

new_console::
	dotnet new console -o $(NAME)
	$(CMD_ADD_TO_SOLUTION)

add_ref::
	dotnet add $(PROJ_TO) reference $(PROJ)

build::
	dotnet build

run::
	cd $(RUN_PRJ); dotnet run --project $(RUN_PRJ).csproj

tests::
	cd $(RUN_TEST); dotnet test
