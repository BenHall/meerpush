# Welcome to MeerPush.

MeerPush is a fast and effective way to deploy websites to IIS with built-in support for IIS6 and IIS7. 

* NOTE: MeerPush takes full advantage of IronRuby, allowing a flexible ruby dsl while using the power of the .NET framework to deploy the sites.*

## How To Install MeerPush From Gemcutter Gems:



If you would like to install the current, stable release of MeerPush, you can do so easily through the Gemcutter gem server. 

Follow these simple instructions and you will be good to go.



**Step 1:** Setup Gemcutter as a gem source



> gem source -a http://gemcutter.org


(note: you only need to do this once for any given computer that is going to install gems from gemcutter.)



**Step 2:** Install the MeerPush gem



> igem install meerpush

## Examples
MeerPush currently has two different ways 
### Rake
  require 'meerpush'
   
  task :deploy => [:delete, :create, :start]

  desc "Delete Website"
  delete_site :delete do |w|
      w.name = 'Meerpush_Website'
  end

  desc "Create Website"
  create_site :create => :delete do |w|
      w.name = 'Meerpush_Website'
      w.home = 'C:\inetpub\wwwroot'
  end

  desc "Start Website"
  start_site :start do |w|
      w.name = 'Meerpush_Website'
  end


### Natural language

  Remove website 'test_site' if exists on 'localhost'
  Create website 'test_site' on 'localhost'
  on port '8080'
  with home directory 'C:\inetpub\wwwroot'
  create and start

## Executing
### Rake
* Include the above snippet in your rakefile
* Enter the following command to deploy your site
  irake deploy

### Natural language
* Create a rakefile together with a script file in the format as shown above. 
* Within the rakefile, simply include the code require 'meerpush'
* Execute the script by entering the following command together with the name of the script file:
  irake dsl:deploy script=name_of_deployment_script

## Meerkatalyst
Meerkatalyst is a project with the aim of reducing the cost of defects to zero. By developing a toolset to support the development of software, we aim to make developers and testers more effective at delivering amazing projects. MeerPush is one small part of Meerkatalyst's vision. 