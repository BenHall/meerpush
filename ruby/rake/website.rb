require 'rake'
require 'rake/tasklib'

module Website
  class Create < Rake::TaskLib
      attr_accessor :name

      def initialize(name = :create)
        @name = name
        @w = MeerPush::IIS7::WebsiteController.new

        define
      end

      def define
        yield @w if block_given?
        task name do
          Website.log 'Creating website'
          @w.create
        end
      end
  end
end
