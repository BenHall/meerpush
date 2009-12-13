begin 
  require 'libs/meerpush.dll'
rescue LoadError 
  raise 'Unable to load MeerPush.dll. Ensure you are running MeerPush via IronRuby'
end

require 'rake/website'
require 'rake'
require 'dsl/deploy'
require 'dsl/steps'