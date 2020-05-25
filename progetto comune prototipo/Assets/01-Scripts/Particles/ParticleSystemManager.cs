using System;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class Particle
{
    #region Public Fields
    public string name;

    public ParticleSystem particleEffect;


    [HideInInspector]
    public bool instantiated = false;

    #endregion

    #region Public Methods
    public void StopParticle()
    {
        if (instantiated == true)
        {
            particleEffect.Stop();
        }

    }

    public void Playparticle()
    {
        if (instantiated == true)
        {
            particleEffect.Play();
        }
    }

    public IEnumerator StopParticleWithDelay(float delay)
    {
        if (instantiated == true)
        {
            yield return new WaitForSeconds(delay);
            particleEffect.Stop();
        }

    }

    public void EmitParticle(int particleNum)
    {
        if (instantiated == true)
        {
            particleEffect.Emit(particleNum);
        }
    }

    #endregion
}

public class ParticleSystemManager : MonoBehaviour
{
    #region Public Field

    public static ParticleSystemManager Instance;

    #endregion

    #region Private Field

    // Array of particles setted by inspector
    [SerializeField]
    private Particle[] particles;

    // Array of particles instantiated in scene from the project folder
    private Particle[] instantiatedparticles;

    #endregion

    #region Awake
    private void Awake()
    {
        Instance = this;
    }

    #endregion

    #region Start
    private void Start()
    {
        instantiatedparticles = new Particle[particles.Length];
        this.InstantiateAllParticles();
    }

    #endregion

    // Instantiate all particles at runtime
    #region Public Methods
    public void InstantiateAllParticles()
    {
        // Loop through every particle in inspector
        for (int i = 0; i < particles.Length; i++)
        {

            // Instantiate  the prefab in the scene with the same position and rotation values
            ParticleSystem iParticle = Instantiate(particles[i].particleEffect, new Vector3(particles[i].particleEffect.transform.position.x,
                                                                                    particles[i].particleEffect.transform.position.y,
                                                                                    particles[i].particleEffect.transform.position.z),
                                                                        new Quaternion(particles[i].particleEffect.transform.rotation.x,
                                                                                       particles[i].particleEffect.transform.rotation.y,
                                                                                       particles[i].particleEffect.transform.rotation.z,
                                                                                       particles[i].particleEffect.transform.rotation.w));
            // Set all the particles prefab as childrean of the ParticleSystemManager to make them easy to access from Hierarchy
            iParticle.transform.SetParent(this.transform);

            // Populate the array of instantiated particle to make a reference to in scene prefabs ("particles" array has the refences to the not instatiated ones)
            instantiatedparticles[i] = new Particle
            {
                name = particles[i].name,
                particleEffect = iParticle,
                instantiated = true,
            };

        }
    }

    // Searches a specified instantiated particle inside the respective array and returns it 
    public Particle FindParticle(string name)
    {
        Particle p = Array.Find(instantiatedparticles, par => par.name == name);
        if (p == null)
        {
            Debug.LogError("Particle " + name + " not found!");
            throw new NullReferenceException();
        }
        return p;
    }

    // Start the particle system
    public void ActivateParticle(string name)
    {
        this.FindParticle(name).Playparticle();
    }

    // Stop the particle system
    public void DeactivateParticle(string name)
    {
        this.FindParticle(name).StopParticle();
    }

    // Emit a fixed number of particle from the particle system
    public void EmitParticles(string name, int particlesNum)
    {
        this.FindParticle(name).EmitParticle(particlesNum);
    }

    public void DeactivateParticleWithDelay(string name, float delay)
    {
        Particle p = this.FindParticle(name);
        StartCoroutine(p.StopParticleWithDelay(delay));
    }

    // Set the position of the particle system (put a 0 to leave the default value)
    public void SetPosition(string name, float x = 0, float y = 0, float z = 0)
    {
        Particle p = this.FindParticle(name);

        if (x == 0)
        {
            x = p.particleEffect.transform.position.x;
        }

        if (y == 0)
        {
            y = p.particleEffect.transform.position.y;
        }

        if (z == 0)
        {
            z = p.particleEffect.transform.position.z;
        }

        p.particleEffect.transform.position = new Vector3(x, y, z);
    }

    // Set the rotation of the particle system (put a 0 to leave the default value)
    public void SetRotation(string name, float x = 0, float y = 0, float z = 0)
    {
        Particle p = this.FindParticle(name);

        if (x == 0)
        {
            x = p.particleEffect.transform.rotation.x;
        }

        if (y == 0)
        {
            y = p.particleEffect.transform.rotation.y;
        }

        if (z == 0)
        {
            z = p.particleEffect.transform.rotation.z;
        }

        p.particleEffect.transform.rotation = Quaternion.Euler(x, y, z);
    }

    #endregion

}
