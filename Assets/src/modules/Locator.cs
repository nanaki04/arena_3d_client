using System.Collections.Generic;
using System;
using UnityEngine;

namespace Arena.Modules {
  public class LocatorTarget {

  }

  public struct PlugState {
    public string Domain;
    public string Address;
    public ImMap<string, ImMap<string, LocatorTarget>> RegistrationList;

    public PlugState(
      string domain,
      string address,
      ImMap<string, ImMap<string, LocatorTarget>> registrationList
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
    public ImMap<string, ImMap<string, LocatorTarget>> RegistrationList { get; }
    public ImList<LocatorPlug> Plugs { get; }

    public Locator(
      ImMap<string, ImMap<string, LocatorTarget>> registrationList,
      ImList<LocatorPlug> plugs
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
      return Im.Fold<LocatorPlug, PlugState>(TransformByPlug, plugState, Plugs);
    }

    private PlugState TransformByPlug(PlugState plugState, LocatorPlug plug) {
      return plug.Transform(plugState);
    }

    private LocatorTarget Wrap(LocatorTarget target) {
      return Im.Fold<LocatorPlug, LocatorTarget>(WrapByPlug, target, Plugs);
    }

    private LocatorTarget WrapByPlug(LocatorTarget target, LocatorPlug plug) {
      return plug.Wrap(target);
    }

    private ImMap<string, LocatorTarget> GetTargetList(
      string domain,
      ImMap<string, ImMap<string, LocatorTarget>> registrationList
    ) {
      if (!registrationList.Has(domain)) {
        return Im.Map<LocatorTarget>();
      }
      return RegistrationList[domain];
    }

    private LocatorTarget GetTargetFromTargetList(
      string address,
      ImMap<string, LocatorTarget> targetList
    ) {
      if (!targetList.Has(address)) {
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
