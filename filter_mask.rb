require 'fluent/plugin/filter'

module Fluent::Plugin
  class MaskFilter < Filter

    Fluent::Plugin.register_filter('mask', self)

    def initialize
      super
    end

    def configure(conf)
      super
    end

    def filter(tag, time, record)
      begin
        mask(record)
      rescue => e
        router.emit_error_event(tag, time, record, e)
      end
    end

    def mask(record)
      newrecord = {}
      
      record.each do |key, value|
        key = key.gsub(/\[|\]/, '')
        
        if value.is_a? String
            if  key == "Statement.internalAccountId" or key == "Account.Account Number" or key == "Account.External Account Number"
                value = value.gsub(/.(?=.{4})/,'#')
            elsif key == "Account.Account Numbers"
                valueArr = value.gsub(/"|\[|\]/, '').split(',').map{|acc| acc = acc.gsub(/.(?=.{4})/,'#')}
                if valueArr.any?
                    value = "[\"#{ valueArr.join("\",\"")}\"]"
                end
            end 
        elsif value.is_a? Hash
          if value.key?("name") and value.key?("value")  and value["name"] == "Account.Account Number"
            value["value"] = value["value"].gsub(/.(?=.{4})/,'#')
          elsif value["name"] == "Account.Account Numbers" 
            valueArr = value["value"].gsub(/"|\[|\]/, '').split(',').map{|acc| acc = acc.gsub(/.(?=.{4})/,'#')}
            if valueArr.any?
                value["value"] = "[\"#{ valueArr.join("\",\"")}\"]"
            end 
          end
          value = mask value
        elsif value.is_a? Array
          value = value.map { |v| v.is_a?(Hash) ? mask(v) : v }
        end

        newrecord[key] = value
      end

      newrecord
    end

  end
end
