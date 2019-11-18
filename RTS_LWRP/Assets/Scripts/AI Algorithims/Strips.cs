/*
 * File: Strips.cs
 * File Created: Monday, 18th November 2019 10:17:22 am
 * ––––––––––––––––––––––––
 * Author: Jesus Fermin, 'Pokoi', Villar  (hello@pokoidev.com)
 * ––––––––––––––––––––––––
 * MIT License
 * 
 * Copyright (c) 2019 Pokoidev
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of
 * this software and associated documentation files (the "Software"), to deal in
 * the Software without restriction, including without limitation the rights to
 * use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
 * of the Software, and to permit persons to whom the Software is furnished to do
 * so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strips 
{
    public  List<OperatorStrips> currentPlan;
    private List<OperatorStrips> operators;
    private List<PropertyStrips> ownProperties;

    private List<string> desiredTags;

    public Strips()
    {
        currentPlan     = new List<OperatorStrips>();
        operators       = new List<OperatorStrips>();
        ownProperties   = new List<PropertyStrips>();
        desiredTags     = new List<string>();
        
    }

    public void AddProperty(PropertyStrips propertyStrips)
    {
        if (!ownProperties.Contains(propertyStrips))
        {
            ownProperties.Add(propertyStrips);
        }
    }

    public string GetNextAction()
    {   
        desiredTags.Add("Goal");

        Analize();                    
        
        OperatorStrips nextOperation = currentPlan[0];            
        return nextOperation.GetFunctionName();            
    }

    private void Analize()
    {
        currentPlan.Clear();            

        while (desiredTags.Count != 0)
        {
            foreach (OperatorStrips stripsOperator in operators)
            {
                List<string> ownedTags = new List<string>();
                
                foreach (PropertyStrips property in ownProperties) 
                {
                    ownedTags.Add(property.GetTag());
                }

                int iterator = 0;

                List <PropertyStrips> added_conditions = stripsOperator.GetAddedProperties();  
                
                for (iterator = 0; iterator < stripsOperator.GetAddedProperties().Count; iterator++)
                {
                    if (desiredTags.Contains(added_conditions[iterator].GetTag()) && !currentPlan.Contains(stripsOperator))
                    {
                        currentPlan.Add(stripsOperator);
                        foreach (PropertyStrips condition in stripsOperator.GetPreconditions()) 
                        {
                            if (!ownedTags.Contains(condition.GetTag()))
                            {
                                 desiredTags.Add(condition.GetTag());    
                            }
                        }                        
                        foreach (PropertyStrips added in stripsOperator.GetAddedProperties()) 
                        {
                            if (desiredTags.Contains(added.GetTag())) 
                            {
                                desiredTags.Remove(added.GetTag());
                            }
                        }
                    }
                }            
            }
            
        }

        currentPlan.Reverse();
    }

}

public class PropertyStrips
{
    string tag;

    public PropertyStrips (string tag)  => this.tag = tag;
    public string GetTag()              => tag;
}

public class OperatorStrips
{
    private List<PropertyStrips> preconditions;
    private List<PropertyStrips> addedProperties;
    private List<PropertyStrips> eliminatedProperties;

    string functionName;

    public OperatorStrips(List<PropertyStrips> preconditions, string resultTag, string functionName)
    {
        this.preconditions   = preconditions;
        this.addedProperties = new List<PropertyStrips>{new PropertyStrips(resultTag)};
        this.functionName    = functionName;
    }

    public List<PropertyStrips> GetAddedProperties()    => addedProperties;
    public List<PropertyStrips> GetPreconditions()      => preconditions;
    public string GetFunctionName()                     => functionName;
}
