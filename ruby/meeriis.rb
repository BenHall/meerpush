require 'rake'
require 'rake/tasklib'
require '../meeriis/bin/Debug/meeriis.dll'

module Website

   class Create < Rake::TaskLib
      attr_accessor :name
   
      def initialize(name = :create)
        @name = name
        @w = MeerIIS::IIS7::Website.new 
        @w.Server = 'localhost'
        
        yield @w if block_given?
        define
      end
      
      def define
        task name do
          'Creating website'
          @w.create
        end
      end 
   end
end
