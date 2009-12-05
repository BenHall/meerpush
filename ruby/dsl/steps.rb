def remove_website(name, server='localhost')
    puts "Deleting website #{name} on #{server}"
    @w = MeerPush::IIS6::WebsiteController.new
    @site = MeerPush::Website.new
	@site.server = server
    @site.name = name
    @w.Site = @site
    @w.delete if @w.exists
end

def create_website(name, server='localhost')
    puts "Creating website #{name} on #{server}"
	@w = MeerPush::IIS6::WebsiteController.new
    @site = MeerPush::Website.new
	@site.server = server
    @site.name = name
end

def with_home_directory(dir)
	puts "With home directory #{dir}"
	@site.home = dir
end

def on_server(server)
	puts "On server #{server}"
	@site.server = server
end

def on_port(port)
	puts "On port #{port}"
	@site.port = port.to_i
end

def create_and_start()
	puts "Creating and starting"
    @w.Site = @site
	@w.create
    @w.start
end

def failed_status_message()
  "\n\n*** DEPLOYMENT FAILED ***"
end

def create_missing_message(m, args)
   "The step '#{m}' with #{args.length} arguments is missing.
To define the step, use code similar to below:\n
   def #{m}(#{args.join(', ')})
     #TODO
   end"
end