using System.Collections.Generic;
using System;
using UnityEngine;

namespace Arena.Modules {
  public class LocatorTarget {

  }

  public struct PlugState {
    public string Domain;
    public string Address;
    public Dictionary<string, Dictionary<string, LocatorTarget>> RegistrationList;

    public PlugState(
      string domain,
      string address,
      Dictionary<string, Dictionary<string, LocatorTarget>> registrationList
    ) {
      Domain = domain;
      Address = address;
      RegistrationList = registrationList;
    }
  }

  public interface LocatorPlug {
    PlugState Transform(PlugState plugState);
    LocatorTarget Wrap(LocatorTarget locatorTarget);
  }

  public class Locator {
    Dictionary<string, Dictionary<string, LocatorTarget>> RegistrationList;
    List<LocatorPlug> Plugs;

    public Locator(
      Dictionary<string, Dictionary<string, LocatorTarget>> registrationList,
      List<LocatorPlug> plugs
    ) {
      RegistrationList = registrationList;
      Plugs = plugs;
    }

    public LocatorTarget Locate(string domain, string address) {
      var plugState = CreatePlugState(domain, address);
      plugState = Transform(plugState);
      var locatorTarget = GetTarget(plugState);
      return Wrap(locatorTarget);
    }

    private PlugState Transform(PlugState plugState) {
      foreach (LocatorPlug plug in Plugs) {
        plugState = plug.Transform(plugState);
      }
      return plugState;
    }

    private LocatorTarget Wrap(LocatorTarget target) {
      foreach (LocatorPlug plug in Plugs) {
        target = plug.Wrap(target);
      }
      return target;
    }

    private Dictionary<string, LocatorTarget> GetTargetList(
      string domain,
      Dictionary<string, Dictionary<string, LocatorTarget>> registrationList
    ) {
      if (!registrationList.ContainsKey(domain)) {
        return new Dictionary<string, LocatorTarget>();
      }
      return RegistrationList[domain];
    }

    private LocatorTarget GetTargetFromTargetList(
      string address,
      Dictionary<string, LocatorTarget> targetList
    ) {
      if (!targetList.ContainsKey(address)) {
        // MEMO: do error handling inside a plug
        return new LocatorTarget();
      }
      return targetList[address];
    }

    private LocatorTarget GetTarget(PlugState plugState) {
      var targetList = GetTargetList(plugState.Domain, plugState.RegistrationList);
      return GetTargetFromTargetList(plugState.Address, targetList);
    }

    private PlugState CreatePlugState(string domain, string address) {
      // TODO clone RegistrationList for safety
      return new PlugState(domain, address, RegistrationList);
    }

    private PlugState TransformIdentity(PlugState plugState) {
      return plugState;
    }

    private LocatorTarget WrapperIdentity(LocatorTarget locatorTarget) {
      return locatorTarget;
    }
  }

}
