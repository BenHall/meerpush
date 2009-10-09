desc "create"
task :create do
    File.readlines('script').map do |line| 
		method = get_method line
		arg = get_argument line
		eval("#{method} '#{arg}'") unless arg.nil? 
		eval("#{method}") if arg.nil?
	end
end

def get_argument(method)
	r = /\w+\s'(.*?)'/
	method =~ r
	result = Regexp.last_match(1) unless Regexp.last_match.nil?
end

def get_method(line)
	 m_no_param = line
     argIndex = line.index("'")
	 unless argIndex.nil?
		index = argIndex - 1
		m_no_param = m_no_param[0, index]
	 end
	 m_no_param.downcase.gsub(' ', '_')
end