namespace :dsl  do
  desc "Creating IIS site from script"
  task :create do
      to_call = []
      missing = []
      File.readlines('dsl\script').map do |line|
          method = get_method line
          args = get_arguments line
          defined = is_method_defined? method, args

          method_to_call = "#{method}" 
          method_to_call = "#{method} #{args.join(', ')}" unless args.nil?

          if(defined)
              to_call << method_to_call
          else
              puts create_missing_message(method, args)
              missing << method_to_call
          end
      end

    eval_or_fail to_call, missing
  end

  def eval_or_fail(methods, missing)
   raise failed_status_message if missing.length > 0
   
   eval_methods methods
  end

  def eval_methods(methods)
      methods.each {|m| eval(m)}
  end

  def is_method_defined?(method, args)
    found = false
    begin
      found = method(method).arity == args.length || method(method).arity == -2
    rescue
      found = false
    end

    return found
  end

  def get_arguments(method)
    return method.scan(/'[^']*'/)
  end

  def get_method(line)
       m_no_param = line
       




       arg_index = line.index("'")
       unless arg_index.nil?
          index = arg_index - 1
          m_no_param = m_no_param[0, index]
       end
       m_no_param.downcase.gsub(' ', '_')
  end
end