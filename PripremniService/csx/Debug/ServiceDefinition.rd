<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="PripremniService" generation="1" functional="0" release="0" Id="51c79691-c38c-49dc-8c4a-93c679167db5" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="PripremniServiceGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="JobWorker:InputRequest" protocol="tcp">
          <inToChannel>
            <lBChannelMoniker name="/PripremniService/PripremniServiceGroup/LB:JobWorker:InputRequest" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="JobWorker:DataConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/PripremniService/PripremniServiceGroup/MapJobWorker:DataConnectionString" />
          </maps>
        </aCS>
        <aCS name="JobWorker:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/PripremniService/PripremniServiceGroup/MapJobWorker:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="JobWorkerInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/PripremniService/PripremniServiceGroup/MapJobWorkerInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:JobWorker:InputRequest">
          <toPorts>
            <inPortMoniker name="/PripremniService/PripremniServiceGroup/JobWorker/InputRequest" />
          </toPorts>
        </lBChannel>
        <sFSwitchChannel name="SW:JobWorker:InternalRequest">
          <toPorts>
            <inPortMoniker name="/PripremniService/PripremniServiceGroup/JobWorker/InternalRequest" />
          </toPorts>
        </sFSwitchChannel>
      </channels>
      <maps>
        <map name="MapJobWorker:DataConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/PripremniService/PripremniServiceGroup/JobWorker/DataConnectionString" />
          </setting>
        </map>
        <map name="MapJobWorker:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/PripremniService/PripremniServiceGroup/JobWorker/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapJobWorkerInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/PripremniService/PripremniServiceGroup/JobWorkerInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="JobWorker" generation="1" functional="0" release="0" software="C:\Users\Zvezdana\Desktop\CLOUD\PripremniZadatak\PripremniZadatak\PripremniService\PripremniService\csx\Debug\roles\JobWorker" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="-1" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="InputRequest" protocol="tcp" portRanges="10100" />
              <inPort name="InternalRequest" protocol="tcp" />
              <outPort name="JobWorker:InternalRequest" protocol="tcp">
                <outToChannel>
                  <sFSwitchChannelMoniker name="/PripremniService/PripremniServiceGroup/SW:JobWorker:InternalRequest" />
                </outToChannel>
              </outPort>
            </componentports>
            <settings>
              <aCS name="DataConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;JobWorker&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;JobWorker&quot;&gt;&lt;e name=&quot;InputRequest&quot; /&gt;&lt;e name=&quot;InternalRequest&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/PripremniService/PripremniServiceGroup/JobWorkerInstances" />
            <sCSPolicyUpdateDomainMoniker name="/PripremniService/PripremniServiceGroup/JobWorkerUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/PripremniService/PripremniServiceGroup/JobWorkerFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="JobWorkerUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="JobWorkerFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="JobWorkerInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="62929357-7cec-40cf-9899-5e6eb8726087" ref="Microsoft.RedDog.Contract\ServiceContract\PripremniServiceContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="c451ba13-4f48-493f-a603-0188b16ddc7b" ref="Microsoft.RedDog.Contract\Interface\JobWorker:InputRequest@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/PripremniService/PripremniServiceGroup/JobWorker:InputRequest" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>