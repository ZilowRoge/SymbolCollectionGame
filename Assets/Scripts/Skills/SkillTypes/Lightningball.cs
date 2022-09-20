using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Lightningball : SimpleProjectile
{
    public GameObject electric_arc_object;
    public int number_of_arcs;
    public float arc_duration;
    public float arc_hit_distance = 10.0f;
    public float main_damage;
    public float arc_damage;

    public LayerMask ignore_layer;
    public LayerMask enemy_layer;

    private List<GameObject> arcs_list;
    private HashSet<Transform> enemy_targets;

    protected override void Start()
    {
        arcs_list = new List<GameObject>(number_of_arcs);
        enemy_targets = new HashSet<Transform>();
    }

    protected override void Update()
    {
        base.Update();
        arcs_list.RemoveAll(item => item == null);
        enemy_targets.RemoveWhere(enemy => arcs_list.Find(arc => arc.GetComponent<VisualEffect>().GetInt("TargetHashCode") == enemy.GetHashCode()) == null);
        for (int i = arcs_list.Count - 1; i >= 0; i--)
        {
            VisualEffect effect = arcs_list[i].GetComponent<VisualEffect>();
            effect.SetVector3("StartPoint", transform.position);
        }

    }


    public override void onCast()
    {
        base.onCast();
        StartCoroutine(createArcs());
    }

    private void searchTargets()
    {
        List<Collider> targets = new List<Collider>(Physics.OverlapSphere(transform.position, arc_hit_distance, enemy_layer));
        selectRandomTargets(targets);
    }

    private void selectRandomTargets(List<Collider> targets)
    {
        int i = targets.Count - 1;
        for (; i >= 0 && enemy_targets.Count < number_of_arcs;)
        {
            int index = Random.Range(0, i);
            Transform tansform = targets[index].transform;
            if (enemy_targets.Add(tansform))
            {
                Debug.Log("Enemy targets: " + enemy_targets.Count);
                targets.RemoveAt(index);
                i--;
            }
        }
    }
    
    private void createArc(Vector3 start_point, Vector3 end_point, Vector3 arc_direction, int hash_code)
    {
        GameObject arc = Instantiate<GameObject>(electric_arc_object);
        VisualEffect visual_effect = arc.GetComponent<VisualEffect>();
        visual_effect.SetInt("TargetHashCode", hash_code);
        visual_effect.SetVector3("StartPoint", start_point);
        visual_effect.SetVector3("EndPoint", end_point);
        visual_effect.SetVector3("ArcDirection", arc_direction);
        arcs_list.Add(arc);
        GameObject.Destroy(arc, Random.Range(0.05f, arc_duration));
    }
    IEnumerator createArcs()
    {
        while (true)
        {
            searchTargets();
            //Debug.Log("Number of targets:" + enemy_targets.Count);
            if (arcs_list.Count <= number_of_arcs)
            {
                foreach (Transform target in enemy_targets)
                {
                    createArc(transform.position, target.position, target.position - transform.position, target.gameObject.GetHashCode());
                    target.GetComponent<Enemy>().onDamage(arc_damage);
                }
                for(int i = arcs_list.Count; i <= number_of_arcs; i++)
                {
                    Vector3 direction = Random.insideUnitSphere;
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, direction, out hit, arc_hit_distance, ~ignore_layer))
                    {
                        createArc(transform.position, hit.point, direction, -1);
                    }
                }
            }
            yield return new WaitForSeconds(.3f);
        }
    }

    //protected void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("Collision");
    //    if(collision.gameObject.tag == "Enemy")
    //    {
    //        collision.gameObject.GetComponent<Enemy>().onDamage(main_damage);
    //    }
    //    StopCoroutine(createArcs());
    //    onDestroy();
    //}
}
