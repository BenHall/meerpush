require 'rake'
require 'rake/tasklib'

module Website
  class Create < Rake::TaskLib
      attr_accessor :name

      def initialize(name = :create)
        @name = name
        @w = MeerPush::IIS7::WebsiteController.new
        yield @w.site if block_given?
        define
      end

      def define
        task name do
          Website.log 'Creating website'
          @w.create
        end
      end
  end

  class Delete < Rake::TaskLib
      attr_accessor :name

      def initialize(name = :delete)
        @name = name
        @w = MeerPush::IIS7::WebsiteController.new

        yield @w.site if block_given?
        define
      end

      def define
        task name do
          Website.log 'Deleting website'
          @w.Delete
        end
      end
  end

  class Start < Rake::TaskLib
      attr_accessor :name

      def initialize(name = :start)
        @name = name
        @w = MeerPush::IIS7::WebsiteController.new
        yield @w.site if block_given?
        define
      end

      def define
        task name do
          Website.log 'Starting website'
          @w.start
        end
      end
  end

  def self.log(msg)
    puts msg
  end
end
