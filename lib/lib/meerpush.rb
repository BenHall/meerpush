begin 
  require File.expand_path(File.join(File.dirname(__FILE__), 'meerpush/assemblies/meerpush.dll'))
rescue LoadError 
  raise 'Unable to load MeerPush.dll. Ensure you are running MeerPush via IronRuby'
end

require File.expand_path(File.join(File.dirname(__FILE__), 'meerpush/rake/website'))
require File.expand_path(File.join(File.dirname(__FILE__), 'meerpush/dsl/deploy'))
require File.expand_path(File.join(File.dirname(__FILE__), 'meerpush/dsl/steps'))